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
            Extractor ext = new Extractor("..\\..\\");
            //ExtractFromZIP
            ext.ExtractFromArchive("TravelInfo.zip");
            //PDF Reporter
            PDFReporterGenerator.CreatePDF();
            //JSON Reporter
            Reporter reporter1 = new Reporter();
            reporter1.MakeReports();
            //ExcelReporter
            var reporter = new ExcelReporter();
            reporter.Report();
            var dataReader = new XMLDataInserter();
            dataReader.ParseXML();
        }
    }
}