using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfficientJsonImporter
{


    class DataDumpArchive
    {


        public static string MapProjectPath(string path)
        {
            System.Reflection.Assembly ass = System.Reflection.Assembly.GetExecutingAssembly();
            string basePath = System.IO.Path.GetDirectoryName(ass.Location);
            basePath = System.IO.Path.Combine(basePath, "../../");
            path = System.IO.Path.Combine(basePath, path);
            return System.IO.Path.GetFullPath(path);
        } // End Function MapProjectPath 


        public static int UnitFactor(string unit)
        {
            if (System.StringComparer.InvariantCultureIgnoreCase.Equals(unit, "G"))
                return 1024 * 1024 * 1024;

            if (System.StringComparer.InvariantCultureIgnoreCase.Equals(unit, "M"))
                return 1024 * 1024;

            if (System.StringComparer.InvariantCultureIgnoreCase.Equals(unit, "K"))
                return 1024;

            return 1;
        }


        public static void GetPossibleStackOverflowDataDumps()
        {
            string path = MapProjectPath("HTML/stack_exchange_data_dumps.txt");

            System.Console.WriteLine(path);

            string url = @"https://archive.org/download/stackexchange";

            HtmlAgilityPack.HtmlWeb page = new HtmlAgilityPack.HtmlWeb();
            // HtmlAgilityPack.HtmlDocument doc = page.Load(url);
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.Load(path);

            // System.Console.WriteLine(doc.DocumentNode.OuterHtml);


            // [not(@territory='true')] ./a
            foreach (HtmlAgilityPack.HtmlNode link in doc.DocumentNode.SelectNodes("//pre/a[@href]"))
            {
                if (link.InnerText == "../")
                    continue;

                HtmlAgilityPack.HtmlAttribute att = link.Attributes["href"];

                if (string.IsNullOrEmpty(att.Value))
                    continue;

                string downloadLink = "https://archive.org/download/stackexchange/" + att.Value;

                string dateTimeSize = link.NextSibling.InnerText.Trim();
                string[] arrAttribs = dateTimeSize.Split(new char[] { ' ', '\t' }, System.StringSplitOptions.RemoveEmptyEntries);
                System.Console.WriteLine(arrAttribs);


                string date = arrAttribs[0];
                string time = arrAttribs[1];
                string size = arrAttribs[2];
                size = size.Replace(",", "");
                string unit = size.Substring(size.Length - 1, 1);
                size = size.Substring(0, size.Length - 1);
                int factor = UnitFactor(unit);
                double d = 0;
                if (double.TryParse(size, out d))
                {
                    d *= factor;
                    d = System.Math.Ceiling(d);
                }
                System.Console.WriteLine(d);

                System.Console.WriteLine(date + ": " + downloadLink);
            }

            System.Console.WriteLine("Finished ! ");
        }

    }
}
