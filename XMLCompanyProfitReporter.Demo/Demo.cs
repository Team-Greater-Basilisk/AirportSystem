using Airport.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLCompanyProfitReporter.Demo
{
    class Demo
    {
        static void Main(string[] args)
        {
            using (var contex = new AirportDbContext())
            {
                ProfitReporter.CreateXMLProfitReport("test.xml", contex, DateTime.MinValue, DateTime.MaxValue);
            }

        }
    }
}
