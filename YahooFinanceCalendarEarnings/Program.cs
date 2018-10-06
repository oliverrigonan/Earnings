using HtmlAgilityPack;
using System;
using System.Linq;
using System.Net;

namespace YahooFinanceCalendarEarnings
{
    class Program
    {
        static void Main(string[] args)
        {
            WebClient webClient = new WebClient();
            string page = webClient.DownloadString("https://finance.yahoo.com/calendar/earnings?day=2018-10-03");

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(page);

            foreach (HtmlNode table in doc.DocumentNode.SelectNodes("//table[contains(@class, 'data-table W(100%) Bdcl(c) Pos(r) BdB Bdc($c-fuji-grey-c)')]"))
            {
                foreach (HtmlNode tableBody in table.SelectNodes("tbody"))
                {
                    Console.WriteLine("No. of Earnings: " + tableBody.SelectNodes("tr").Count());
                    Console.WriteLine();

                    foreach (HtmlNode tableRow in tableBody.SelectNodes("tr"))
                    {
                        HtmlNodeCollection tableRows = tableRow.SelectNodes("td");
                        if (tableRows.Any())
                        {
                            Console.WriteLine("Symbol: " + tableRows[1].InnerText);
                            Console.WriteLine("Company: " + tableRows[2].InnerText);
                            Console.WriteLine("Earnings Call Time: " + tableRows[3].InnerText);
                            Console.WriteLine("EPS Estimate: " + tableRows[4].InnerText);
                            Console.WriteLine("Reported EPS: " + tableRows[5].InnerText);
                            Console.WriteLine("Surprise (%): " + tableRows[6].InnerText);
                            Console.WriteLine();
                        }
                    }
                }
            }

            Console.ReadKey();
        }
    }

    class Earnings
    {
        public String Symbol { get; set; }
        public String Company { get; set; }
        public String EarningsCallTime { get; set; }
        public String EPSEstimate { get; set; }
        public String ReportedEPS { get; set; }
        public String SurprisePercentage { get; set; }
    }
}
