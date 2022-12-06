using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace WebScrapper
{
    class Program
    {
        private static Timer timer;
        static void Main(string[] args)
        {
            timer = new Timer(_ =>
            {
                try
                {
                    Console.Write(".");
                    var usage = LoadUsage();
                    if (string.IsNullOrEmpty(usage) == false)
                        File.AppendAllText(Path.Combine(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName), "aqua.csv"), $"{DateTime.Now.ToString("s")};{usage}\r\n");


                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }, null, 0, 10000);
            //var usage = LoadUsage();
            //Console.WriteLine(usage);
            //LoadTempLabe();

            Console.WriteLine("Press Enter to end.....");
            Console.ReadLine();
            //void LoadTempLabe()
            //{
            //    var htmlTemp = @"https://hydro.chmi.cz/hpps/popup_hpps_prfdyn.php?seq=307338";
            //    HtmlWeb webTemp = new HtmlWeb();
            //    var htmlDocTemp = webTemp.Load(htmlTemp);
            //    var div = htmlDocTemp.DocumentNode.SelectSingleNode("//div[@class='tborder center_text']");
            //    var table = div.SelectSingleNode("//table");
            //    //var table = htmlDocTemp.DocumentNode.SelectSingleNode("//table");
            //    var tableRows = table.SelectNodes("tr");
            //    var columns = tableRows[0].SelectNodes("th/text()");
            //    for (int i = 1; i < tableRows.Count; i++)
            //    {
            //        for (int e = 0; e < columns.Count; e++)
            //        {
            //            var value = tableRows[i].SelectSingleNode($"td[{e + 1}]");
            //            Console.WriteLine(columns[e].InnerText + ":" + value.InnerText);

            //        }
            //        Console.WriteLine();

            //    }

            //return table.InnerHtml;



            //foreach (var item in htmlDocTemp.DocumentNode.SelectNodes("//div[@class='tborder center_text']"))
            //{
            //    return item.InnerHtml.ToString();
            //}
            //return valueTemp;
        }

    

        private static string LoadUsage()
        {
            var html = @"https://www.aquapce.cz/";
            HtmlWeb web = new HtmlWeb();
            var htmlDoc = web.Load(html);
            foreach (var item in htmlDoc.DocumentNode.SelectNodes("//div[@class='fast-info']"))
            {
                return string.Join(";", Regex.Split(item.InnerText.Trim(), @"\D+")).Trim(';');
            }                  
            
            return "";
        }

        public void LoadTempLabe()
        {
            var htmlTemp = @"https://hydro.chmi.cz/hpps/popup_hpps_prfdyn.php?seq=307338";
            HtmlWeb webTemp = new HtmlWeb();
            var htmlDocTemp = webTemp.Load(htmlTemp);
            var table = htmlDocTemp.DocumentNode.SelectSingleNode("//table");
            var tableRows = table.SelectNodes("tr");
            var columns = tableRows[0].SelectNodes("th/text()");
            for (int i = 1; i < tableRows.Count; i++)
            {
                for (int e = 0; e < columns.Count; e++)
                {
                    var value = tableRows[i].SelectSingleNode($"td[{e + 1}]");
                    Console.WriteLine(columns[e].InnerText + ":" + value.InnerText);

                }
                Console.WriteLine();

            }
            
            //return table.InnerHtml;

           

            //foreach (var item in htmlDocTemp.DocumentNode.SelectNodes("//div[@class='tborder center_text']"))
            //{
            //    return item.InnerHtml.ToString();
            //}
            //return valueTemp;
        }
    }
}
