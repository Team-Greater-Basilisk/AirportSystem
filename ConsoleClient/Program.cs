using Airport.Data;
using MongoDBController;
using System;
using System.Linq;
using Airport.Model;
using System.IO.Compression;
using System.Data.OleDb;
using ZipExcelExtractor;
using PDFReporter;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            DataTransferer.TransferDataFromMongoToMsSql();
            Extractor ext = new Extractor("..\\..\\");
            ext.ExtractFromArchive("TravelInfo.zip");
            PDFReporterGenerator.CreatePDF();
        }
    }
}