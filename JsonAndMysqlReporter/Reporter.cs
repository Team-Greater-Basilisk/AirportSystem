namespace JsonAndMysqlReporter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.IO;

    using Telerik.OpenAccess;

    using JsonReportModel;
    using Airport.Model;
    using Newtonsoft.Json;

    public class Reporter
    {
        private string directoryPath = "JsonReports";

        public void MakeReports()
        {
            UpdateDatabase();

            AddRecordsToMySQLAndJson();
        }

        private void AddRecordsToMySQLAndJson()
        {
            List<Report> records = GetRecords();

            using (var context = new JsonReportModel.FluentModel())
            {

                for (int i = 0; i < records.Count; i++)
                {
                    context.Add(records[i]);
                    context.SaveChanges();

                    AddToJsonFiles(records[i]);
                }
            }
        }

        private List<Report> GetRecords()
        {
            var destinationFrom = "Sphia";
            var reports = new List<Report>();

            using (var db = new AirportDbContext())
            {
                //Tickets.Select(t => t);
                var data = db.Tickets
                             .Join(db.Destinations,
                                    t => t.CustomerId,
                                    d => d.Id,
                                    (t, d) => new
                                                {
                                                    Destination = d.Name,
                                                    TravelDate = t.TravelingDate
                                                })
                              .GroupBy(x => new { x.Destination, x.TravelDate.Year },
                                                    x => new { x.TravelDate },
                                                    (key, groups) => new
                                                                        {
                                                                            Destination = key.Destination,
                                                                            Yuer = key.Year,
                                                                            TiketCount = groups.Count()
                                                                        });



                foreach (var item in data)
                {
                    Report newRekord = new Report()
                    {
                        From = destinationFrom,
                        To = item.Destination,
                        SellTicketsCount = item.TiketCount,
                        Year = item.Yuer
                    };

                    reports.Add(newRekord);
                }
            }

            //for (int i = 0; i < 15; i++)
            //{
            //    reports.Add(new Report()
            //    {
            //        From = "aasas",
            //        To = "daya",
            //        SellTiketsCount = 50,
            //        Year = 2014
            //    });
            //}

            return reports;
        }

        private void AddToJsonFiles(Report record)
        {
            string json = JsonConvert.SerializeObject(record, Formatting.Indented);

            var path = string.Format("{0}/{1}.json", directoryPath, record.ReportID);

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory("JsonReports");
            }

            File.WriteAllText(path, json);
        }

        private static void UpdateDatabase()
        {
            using (var context = new JsonReportModel.FluentModel())
            {
                var schemaHandler = context.GetSchemaHandler();
                EnsureDB(schemaHandler);
            }
        }

        private static void EnsureDB(ISchemaHandler schemaHandler)
        {
            string script = null;
            if (schemaHandler.DatabaseExists())
            {
                script = schemaHandler.CreateUpdateDDLScript(null);
            }
            else
            {
                schemaHandler.CreateDatabase();
                script = schemaHandler.CreateDDLScript();
            }

            if (!string.IsNullOrEmpty(script))
            {
                schemaHandler.ExecuteDDLScript(script);
            }
        }

    }
}
