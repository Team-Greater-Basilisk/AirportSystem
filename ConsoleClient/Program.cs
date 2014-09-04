using Airport.Data;
using MongoDBController;
using System;
using System.Linq;
using Airport.Model;
using System.IO.Compression;
using System.Data.OleDb;
using ZipExcelExtractor;
using PDFReporter;
using JsonAndMysqlReporter;
using XMLDataReader;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //TransferFromMongoToMSSql
            DataTransferer.TransferDataFromMongoToMsSql();
            Console.WriteLine("Data successfully transfered from MongoDB to MSSql");
            Extractor ext = new Extractor("..\\..\\");
            //ExtractFromZIP
            ext.ExtractFromArchive("TravelInfo.zip");
            Console.WriteLine("Data successfully transfered from Excel to MSSql");
            //PDF Reporter
            PDFReporterGenerator.CreatePDF();
            Console.WriteLine("PDF Reports Created");
            //JSON Reporter
            Reporter jsonReporter = new Reporter();
            jsonReporter.MakeReports();
            Console.WriteLine("JSON Reports Created");
            //ExcelReporter
            var reporter = new ExcelReporter();
            reporter.Report();
            var dataReader = new XMLDataInserter();
            dataReader.ParseXML();
            Console.WriteLine("Addition info transfered From XML to MongoDB and MSSQL");
        }
    }
}