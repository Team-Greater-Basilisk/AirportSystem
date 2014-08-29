using Airport.Data;
using MongoDBController;
using System;
using System.Linq;
using Airport.Model;
using System.IO.Compression;
using System.Data.OleDb;
using ZipExcelExtractor;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Extractor ext = new Extractor("..\\..\\");
            ext.ExtractFromArchive("TravelInfo.zip");
        }
    }
}