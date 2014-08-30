namespace PDFReporter
{
    using Airport.Model;
    using Spire.Pdf;
    using Spire.Pdf.Graphics;
    using Spire.Pdf.Tables;
    using System;
    using System.Drawing;
    using System.Globalization;
    using System.Linq;

    public class PDFReporterGenerator
    {
        public static void CreatePDF()
        {
            //Create a pdf document.
            PdfDocument doc = new PdfDocument();
            //margin
            PdfUnitConvertor unitCvtr = new PdfUnitConvertor();
            PdfMargins margin = new PdfMargins();
            margin.Top = unitCvtr.ConvertUnits(2.54f, PdfGraphicsUnit.Centimeter, PdfGraphicsUnit.Point);
            margin.Bottom = margin.Top;
            margin.Left = unitCvtr.ConvertUnits(3.17f, PdfGraphicsUnit.Centimeter, PdfGraphicsUnit.Point);
            margin.Right = margin.Left;
            // Create new page
            PdfPageBase page = doc.Pages.Add(PdfPageSize.A4, margin);
            float y = 10;
            //title
            PdfBrush brush1 = PdfBrushes.Black;
            PdfTrueTypeFont font1 = new PdfTrueTypeFont(new Font("Arial", 16f, FontStyle.Bold));
            PdfStringFormat format1 = new PdfStringFormat(PdfTextAlignment.Center);
            page.Canvas.DrawString("Customer Report", font1, brush1, page.Canvas.ClientSize.Width / 2, y, format1);
            y = y + font1.MeasureString("Customer Report", format1).Height;
            y = y + 5;
            String[][] dataSource;
            using (var db = new AirportDbContext())
            {
                var tickets = db.Tickets.Select(x => new { Company = x.Company.Name, Destination = x.Destination.Name, CustomerName = x.Customer.FirstName + " " + x.Customer.LastName, Date = x.TravelingDate, Price = x.Price.ToString() });
                var counter = 1;
                dataSource = new String[tickets.Count()+1][];
                dataSource[0] = new string[] { "Company", "Destination", "Customer", "Date", "Price" };
                foreach (var ticket in tickets)
                {
                    var date = ticket.Date.Date.ToShortDateString();
                    dataSource[counter] = new string[] { ticket.Company, ticket.Destination, ticket.CustomerName, date, ticket.Price };
                    counter++;
                }
            }
            String[] data
            =
            {
                "Name;Capital;Continent;Area;Population",
                "Argentina;Buenos Aires;South America;2777815;32300003",
                "Bolivia;La Paz;South America;1098575;7300000",
                "Brazil;Brasilia;South America;8511196;150400000",
                "Canada;Ottawa;North America;9976147;26500000",
            };

            PdfTable table = new PdfTable();
            table.Style.CellPadding = 2;
            table.Style.HeaderSource = PdfHeaderSource.Rows;
            table.Style.HeaderRowCount = 1;
            table.Style.ShowHeader = true;
            table.DataSource = dataSource;
            PdfLayoutResult result = table.Draw(page, new PointF(0, y));
            y = y + result.Bounds.Height + 5;
            PdfBrush brush2 = PdfBrushes.Gray;
            PdfTrueTypeFont font2 = new PdfTrueTypeFont(new Font("Arial", 9f));

            //Save pdf file.
            doc.SaveToFile("TicketsReport.pdf");
            doc.Close();
            System.Diagnostics.Process.Start("TicketsReport.pdf");
        }
    }
}