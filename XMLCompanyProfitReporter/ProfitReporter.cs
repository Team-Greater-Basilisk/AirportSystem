using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Airport.Model;
using System.Xml;
namespace XMLCompanyProfitReporter
{
    public static class ProfitReporter
    {
        /// <summary>
        /// Saves the company name, id, and profits for the specified time span in a xml file.
        /// </summary>
        /// <param name="fileName">The full name of the output file.</param>
        /// <param name="airportDbContext">The context used as a connection to the database.</param>
        /// <param name="startDate">The start of the time span it witch the profits will be shown</param>
        /// <param name="endDate">The end of the time span it witch the profits will be shown</param>
        public static void CreateXMLProfitReport(string fileName, AirportDbContext airportDbContext, DateTime startDate, DateTime endDate)
        {
            var companyTicketPriceJoin = airportDbContext.Companies.Join(airportDbContext.Tickets,
                comp => comp.Id, ticket => ticket.CompanyId,
                (company, ticket) => new
                {
                    CompanyName = company.Name,
                    CompanyId = company.Id,
                    TicketPrice = ticket.Price
                });

            var compantyProfitStatistics = companyTicketPriceJoin.GroupBy(
                //Select the items we will group by
                x => new
                { 
                    x.CompanyId,
                    x.CompanyName
                },
                //Select the elements we will need in additional operations
                x => new
                {
                    TicketPrice = x.TicketPrice
                },
                //Construct the result
                (key, groupEl) => new {
                    CompanyId = key.CompanyId,
                    CompanyName = key.CompanyName,
                   Profit = groupEl.Sum(x=>x.TicketPrice)
                });

            using(var xmlReport = XmlTextWriter.Create(fileName))
            {
                xmlReport.WriteStartDocument();
                xmlReport.WriteStartElement("profit-reports");
                foreach (var company in compantyProfitStatistics)
                {
                    xmlReport.WriteStartElement("company");
                    xmlReport.WriteElementString("id", company.CompanyId.ToString());
                    xmlReport.WriteElementString("name", company.CompanyName);
                    xmlReport.WriteElementString("profit", company.Profit.ToString());
                    xmlReport.WriteEndElement();
                }
                xmlReport.WriteEndElement();
            }
        }
    }
    
}
