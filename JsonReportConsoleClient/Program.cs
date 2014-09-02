namespace JsonReportConsoleClient
{
    using JsonAndMysqlReporter;

    class Program
    {
        static void Main()
        {
            Reporter reporter = new Reporter();

            reporter.MakeReports();
        }
    }
}
