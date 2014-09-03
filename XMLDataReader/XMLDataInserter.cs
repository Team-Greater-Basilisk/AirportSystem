using Airport.Data;
using Airport.Model;
using MongoDBController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XMLDataReader
{
    public class XMLDataInserter
    {
        public void ParseXML()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("../../CompanyInfo.xml");
            string xPathQuery = "/companyInfo/company";
            var mongoInserter = new MongoDataInserter();
            XmlNodeList companyList = xmlDoc.SelectNodes(xPathQuery);
            foreach (XmlNode company in companyList)
            {
                var companyName = company.Attributes["name"].Value;
                var town = company.SelectSingleNode("town").InnerText;
                var airplainsCount = company.SelectSingleNode("airplains").InnerText;
                var employeesCount = company.SelectSingleNode("employees").InnerText;

                using (var msDb = new AirportDbContext())
                {
                    var companyFromDb = msDb.Companies.FirstOrDefault(x => x.Name == companyName);
                    var companyInfo = new Airport.Data.CompanyInfo();
                    companyInfo.Town = town;
                    companyInfo.AirplainsCount = int.Parse(airplainsCount);
                    companyInfo.EmployeesCount = int.Parse(employeesCount);
                    companyInfo.Company = companyFromDb;
                    msDb.CompanyInfo.Add(companyInfo);
                    msDb.SaveChanges();
                }

                var mongoInfo = new MongoDBController.CompanyInfo();
                mongoInfo.AirplainsCount = int.Parse(airplainsCount);
                mongoInfo.EmployeesCount = int.Parse(employeesCount);
                mongoInfo.Town = town;
                mongoInfo.CompanyName = companyName;
                mongoInserter.AddCompanyInfo(mongoInfo);
            }
        }
    }
}