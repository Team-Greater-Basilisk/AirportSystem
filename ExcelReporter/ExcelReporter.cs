namespace ConsoleClient
{
    using JsonReportModel;
    using System;
    using System.Collections.Generic;
    using System.Data.OleDb;
    using System.Data.SQLite;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ExcelReporter 
    {
        private const string ExcelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=..\\..\\Report.xlsx;Extended Properties=Excel 12.0;";
        private const string SqliteConnectionString = "Data Source=..\\..\\DestinationInfo.db;Version=3;";
        private const string SqliteToExcelTransferSuccessMessage = "Data transferred from SQLite in Excel file successfully. ";
        private const string MySqlToExcelTransferSuccessMessage = "Data transferred from MySql in Excel file successfully.";

        public void Report()
        {
            this.GetDataFromSqlite();
            this.WriteFromMySqlInExcel();
        }

        public void GetDataFromSqlite()
        {
            var sqliteConnection = new SQLiteConnection(SqliteConnectionString);

            sqliteConnection.Open();

            var command = new SQLiteCommand("SELECT * FROM DestinationInfo", sqliteConnection);
            var reader = command.ExecuteReader();

            using (reader)
            {
                while (reader.Read())
                {
                    string travelTime = (string)reader["Time"];
                    string destination = (string)reader["DestinationName"];
                    

                    InsertInExcel(travelTime, destination);
                }
            }
        }

        private static void InsertInExcel(string travelTime, string destinationName)
        {
            var excelConnection = new OleDbConnection(ExcelConnectionString);
            excelConnection.Open();

            using (excelConnection)
            {
                var insertCommand = new OleDbCommand("INSERT INTO [TravelTime$] (Destination, TravelTime) VALUES (@Destination, @TravelTime)", excelConnection);
                insertCommand.Parameters.AddWithValue("@Destination", destinationName);
                insertCommand.Parameters.AddWithValue("@TravelTime", travelTime);

                insertCommand.ExecuteNonQuery();
            }

            Console.WriteLine(SqliteToExcelTransferSuccessMessage);
        }

        private void WriteFromMySqlInExcel()
        {
            using (var context = new FluentModel())
            {
                var connection = new OleDbConnection(ExcelConnectionString);

                connection.Open();

                var allSalesData = context.Reports;

                using (connection)
                {
                    foreach (var item in allSalesData)
                    {
                        var insertCommand = new OleDbCommand(
                            "INSERT INTO [TicketsByYears$] (From, To, SellTicketsCount, Year) VALUES (@From, @To, @SellTicketsCount, @Year)",
                            connection);
                        insertCommand.Parameters.AddWithValue("@From", item.From);
                        insertCommand.Parameters.AddWithValue("@To", item.To);
                        insertCommand.Parameters.AddWithValue("@SellTicketsCount", item.SellTicketsCount);
                        insertCommand.Parameters.AddWithValue("@Year", item.Year);

                        insertCommand.ExecuteNonQuery();
                    }
                }

                Console.WriteLine(MySqlToExcelTransferSuccessMessage);
            }
        }
    }
}