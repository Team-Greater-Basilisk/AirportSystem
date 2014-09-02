using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonReportModel
{
    public class Report
    {
        public int ReportID { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public int SellTiketsCount { get; set; }

        public int Year { get; set; }

    }
}
