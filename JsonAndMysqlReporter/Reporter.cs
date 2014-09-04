namespace JsonAndMysqlReporter
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    
    using Newtonsoft.Json;
    using Telerik.OpenAccess;

    using Airport.Model;
    using JsonReportModel;
    
    public class Reporter
    {
        private string directoryPath = "../../JsonReports";

        public void MakeReports()
        {
            UpdateDatabase();

            AddRecordsToMySQLAndJson();
        }

        private void AddRecordsToMySQLAndJson()
        {
            List<Report> records = GetRecords();

            using (var context = new JsonReportModel.ReportContext())
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
            var destinationFrom = "Sofia";
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

            return reports;
        }

        private void AddToJsonFiles(Report record)
        {
            string json = JsonConvert.SerializeObject(record, Formatting.Indented);

            var path = string.Format("{0}/{1}.json", directoryPath, record.ReportID);

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory("../../JsonReports");
            }

            File.WriteAllText(path, json);
        }

        private static void UpdateDatabase()
        {
            using (var context = new JsonReportModel.ReportContext())
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
