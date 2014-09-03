using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.Data
{
    public class CompanyInfo
    {
        public int Id { get; set; }
        public string Town { get; set; }

        public int AirplainsCount { get; set; }

        public int EmployeesCount { get; set; }

        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }

    }
}
