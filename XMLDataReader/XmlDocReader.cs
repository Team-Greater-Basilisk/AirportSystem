using Airport.Data;
using Airport.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XMLDataReader
{
    public class XmlDocReader
    {
        public void ReadXmlAndSendToMsSQL(string filename, AirportDbContext dbContext)
        {
            List<Ticket> ticketsFromXml = this.ReadXML(filename);

            foreach (var ticket in ticketsFromXml)
            {
                dbContext.Tickets.Add(ticket);
            }
        }

        private List<Ticket> ReadXML(string filename)
        {
            XmlReader reader = XmlReader.Create(filename);
            List<Ticket> ticketsList = new List<Ticket>();

            using (reader)
            {
                Ticket newTicket = new Ticket();

                while (reader.Read())
                {
                    if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "ticket"))
                    {
                        newTicket = new Ticket();
                    }

                    if ((reader.NodeType == XmlNodeType.EndElement) && (reader.Name == "ticket"))
                    {
                        ticketsList.Add(newTicket);
                    }

                    if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "company"))
                    {
                        if (reader.Read())
                        {
                            var company = new Company();
                            company.Name = reader.Value;
                            newTicket.Company = company;
                        }
                    }

                    if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "customer"))
                    {
                        if (reader.Read())
                        {
                            var customer = new Customer();
                            string[] customerNames = reader.Value.Split(' ');
                            customer.FirstName = customerNames[0];
                            customer.LastName = customerNames[1];
                            newTicket.Customer = customer;
                        }
                    }

                    if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "price"))
                    {
                        if (reader.Read())
                        {
                            newTicket.Price = decimal.Parse(reader.Value);
                        }
                    }

                    if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "travelling-date"))
                    {
                        if (reader.Read())
                        {
                            newTicket.TravelingDate = DateTime.Parse(reader.Value);
                        }
                    }

                    if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "destination"))
                    {
                        if (reader.Read())
                        {
                            var destin = new Destination();
                            destin.Name = reader.Value;
                            newTicket.Destination = destin;
                        }
                    }

                }
            }

            return ticketsList;
        }
    }
}
