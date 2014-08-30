using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Airport.Model;
using Airport.Data;

namespace ZipExcelExtractor
{
    public class Extractor
    {
        private const string FileForExtract = "Reports";
        private string pathToArchive;

        public Extractor(string pathToArchive)
        {
            this.pathToArchive = pathToArchive;
        }

        public void ExtractFromArchive(string archiveName)
        {
            if (Directory.Exists(pathToArchive + FileForExtract))
            {
                Directory.Delete(pathToArchive + FileForExtract, true);
            }
            ZipFile.ExtractToDirectory(pathToArchive + archiveName, pathToArchive + FileForExtract);
            var allFolders = Directory.GetDirectories(pathToArchive +FileForExtract);
            foreach (var folder in allFolders)
            {
                var folderName = Path.GetFileName(folder);
                var allFiles = Directory.GetFiles(folder);
                foreach (var file in allFiles)
                {
                    ExcelParser(folderName , file);
                }
            }
        }

        private void ExcelParser(string folderName , string pathOfFile)
        {            
            var connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 12.0;", pathOfFile);
            OleDbConnection con = new OleDbConnection(connectionString);
            con.Open();
            using (con)
            {
                var dataTable = new DataTable();
                var adapter = new OleDbDataAdapter("select * from [Tickets$] ", con);
                adapter.Fill(dataTable);
                foreach (DataRow row in dataTable.Rows)
                {
                    var companyID = int.Parse(row.ItemArray[0].ToString());
                    var customerID = int.Parse(row.ItemArray[1].ToString());
                    var destinationID = int.Parse(row.ItemArray[2].ToString());
                    var price = decimal.Parse(row.ItemArray[3].ToString());
                    var date = DateTime.Parse(folderName);
                    InsertToDatabase(companyID , customerID , destinationID , price , date);
                }
            }
        }

        private void InsertToDatabase(int companyId , int customerId , int destinationId , decimal price , DateTime date)
        {
            using (var db = new AirportDbContext())
            {
                var ticket = new Ticket();
                ticket.Company = db.Companies.Find(companyId);
                ticket.Customer = db.Customers.Find(customerId);
                ticket.Destination = db.Destinations.Find(destinationId);
                ticket.TravelingDate = date;
                ticket.Price = price;
                

                db.Tickets.Add(ticket);
                db.SaveChanges();
            }
        }
    }
}