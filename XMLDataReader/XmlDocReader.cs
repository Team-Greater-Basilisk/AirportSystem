//using Airport.Data;
//using Airport.Model;
//using MongoDBController;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Xml;

//namespace XMLDataReader
//{
//    public class XmlDocReader
//    {
//        public void ReadXmlAndSendToMsSQL(string filename, AirportDbContext dbContext)
//        {
//            List<object> data = this.ReadXML(filename);

//            if (data.Count > 0 && (data[0] is Ticket))
//            {
//                foreach (var ticket in data)
//                {
//                    Ticket newTicketEntry = ticket as Ticket;
//                    dbContext.Tickets.Add(newTicketEntry);
//                }
//            }

//            if (data.Count > 0 && (data[0] is CompanyInfo))
//            {
//                foreach (var cInfo in data)
//                {
//                    CompanyInfo newCompanyInfo = cInfo as CompanyInfo;
//                    dbContext.CompanyIfno.Add(newCompanyInfo);
//                }
//            }

//            dbContext.SaveChanges();
//        }

//        public void ReadXmlAndSendToMongoDb(string filename, MongoDataInserter mongoDatabase)
//        {
//            List<object> data = this.ReadXML(filename);

//            if (data.Count > 0 && (data[0] is Ticket))
//            {
//                foreach (var ticket in data)
//                {
//                    Ticket newTicketEntry = ticket as Ticket;

//                    Company company = newTicketEntry.Company;
//                    mongoDatabase.AddComapny(company);

//                    Customer customer = newTicketEntry.Customer;
//                    mongoDatabase.AddComapny(customer);

//                    Destination destination = newTicketEntry.Destination;
//                    mongoDatabase.AddComapny(destination);
//                }
//            }

//            if (data.Count > 0 && (data[0] is CompanyInfo))
//            {
//                foreach (var ticket in data)
//                {
//                    CompanyInfo newCompanyInfo = cInfo as CompanyInfo;

//                    //mongoDatabase.AddComapnyInfo(newCompanyInfo);
//                }
//            }
//        }

//        private List<object> ReadXML(string filename)
//        {
//            XmlReader reader = XmlReader.Create(filename);
//            List<object> resultList = new List<object>();

//            using (reader)
//            {
//                while (reader.Read())
//                {
//                    if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "tickets"))
//                    {
//                        resultList = ReadTicketsInfoXML(reader);
//                        break;
//                    }

//                    if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "additional-info"))
//                    {
//                        resultList = ReadAdditionalInfoXML(reader);
//                        break;
//                    }
//                }
//            }

//            return resultList;
//        }

//        private List<object> ReadTicketsInfoXML(XmlReader reader)
//        {
//            List<object> ticketsList = new List<object>();

//            using (reader)
//            {
//                Ticket newTicket = new Ticket();

//                while (reader.Read())
//                {
//                    if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "ticket"))
//                    {
//                        newTicket = new Ticket();
//                    }

//                    if ((reader.NodeType == XmlNodeType.EndElement) && (reader.Name == "ticket"))
//                    {
//                        ticketsList.Add((object)newTicket);
//                    }

//                    if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "company"))
//                    {
//                        if (reader.Read())
//                        {
//                            var company = new Company();
//                            company.Name = reader.Value;
//                            newTicket.Company = company;
//                        }
//                    }

//                    if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "customer"))
//                    {
//                        if (reader.Read())
//                        {
//                            var customer = new Customer();
//                            string[] customerNames = reader.Value.Split(' ');
//                            customer.FirstName = customerNames[0];
//                            customer.LastName = customerNames[1];
//                            newTicket.Customer = customer;
//                        }
//                    }

//                    if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "price"))
//                    {
//                        if (reader.Read())
//                        {
//                            newTicket.Price = decimal.Parse(reader.Value);
//                        }
//                    }

//                    if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "travelling-date"))
//                    {
//                        if (reader.Read())
//                        {
//                            newTicket.TravelingDate = DateTime.Parse(reader.Value);
//                        }
//                    }

//                    if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "destination"))
//                    {
//                        if (reader.Read())
//                        {
//                            var destin = new Destination();
//                            destin.Name = reader.Value;
//                            newTicket.Destination = destin;
//                        }
//                    }

//                }
//            }

//            return ticketsList;
//        }

//        private List<object> ReadAdditionalInfoXML(XmlReader reader)
//        {
//            List<object> additionalInfo = new List<object>();

//            using (reader)
//            {
//                while (reader.Read())
//                {
//                    if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "company"))
//                    {
//                        string companyName = reader["name"];
//                        string companyCountry = string.Empty;
//                        string companyWebSite = string.Empty;
//                        int numberOfEmployees = 0;

//                        while (!((reader.NodeType == XmlNodeType.EndElement) && (reader.Name == "company")))
//                        {
//                            if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "country"))
//                            {
//                                if (reader.Read())
//                                {
//                                    companyCountry = reader.Value;
//                                }
//                            }

//                            if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "website"))
//                            {
//                                if (reader.Read())
//                                {
//                                    companyWebSite = reader.Value;
//                                }
//                            }

//                            if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "number-of-employees"))
//                            {
//                                if (reader.Read())
//                                {
//                                    numberOfEmployees = int.Parse(reader.Value);
//                                }
//                            }

//                            reader.Read();
//                        }

//                        CompanyInfo coInfo = new CompanyInfo();

//                        coInfo.CompanyNameInfo = companyName;
//                        coInfo.CompanyCountryInfo = companyCountry;
//                        coInfo.CompanyWebSiteInfo = companyWebSite;
//                        coInfo.CompanyNumberOfEmployees = numberOfEmployees;

//                        additionalInfo.Add((object)coInfo);
//                    }
//                }
//            }

//            return additionalInfo;
//        }
//    }
//}
