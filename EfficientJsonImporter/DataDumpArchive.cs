
namespace EfficientJsonImporter
{


    public class DataDump
    {
        public string Date;
        public string Time;
        public string HumanReadableSize;
        public long Size;
        public string URL;
        public string FileName;

        public bool IsMeta;
    } // End Class DataDump 


    public class DataDumpArchive
    {


        private static string MapProjectPath(string path)
        {
            System.Reflection.Assembly ass = System.Reflection.Assembly.GetExecutingAssembly();
            string basePath = System.IO.Path.GetDirectoryName(ass.Location);
            basePath = System.IO.Path.Combine(basePath, "../../");
            path = System.IO.Path.Combine(basePath, path);
            return System.IO.Path.GetFullPath(path);
        } // End Function MapProjectPath 


        private static int UnitFactor(string unit)
        {
            if (System.StringComparer.InvariantCultureIgnoreCase.Equals(unit, "G"))
                return 1024 * 1024 * 1024;

            if (System.StringComparer.InvariantCultureIgnoreCase.Equals(unit, "M"))
                return 1024 * 1024;

            if (System.StringComparer.InvariantCultureIgnoreCase.Equals(unit, "K"))
                return 1024;

            return 1;
        } // End Function UnitFactor 


        public static System.Collections.Generic.List<DataDump> GetPossibleStackExchangeDataDumps()
        {
            System.Collections.Generic.List<DataDump> ls = new System.Collections.Generic.List<DataDump>();

            string path = MapProjectPath("HTML/stack_exchange_data_dumps.txt");
            string url = @"https://archive.org/download/stackexchange";
            System.Diagnostics.Debug.WriteLine(path);

            HtmlAgilityPack.HtmlWeb page = new HtmlAgilityPack.HtmlWeb();
            // HtmlAgilityPack.HtmlDocument doc = page.Load(url);
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.Load(path);

            // System.Diagnostics.Debug.WriteLine(doc.DocumentNode.OuterHtml);


            // [not(@territory='true')] ./a
            foreach (HtmlAgilityPack.HtmlNode link in doc.DocumentNode.SelectNodes("//pre/a[@href]"))
            {
                if (link.InnerText == "../")
                    continue;

                HtmlAgilityPack.HtmlAttribute att = link.Attributes["href"];

                if (string.IsNullOrEmpty(att.Value))
                    continue;


                string downloadLink = "https://archive.org/download/stackexchange/" + att.Value;

                if (!downloadLink.EndsWith(".7z", System.StringComparison.InvariantCultureIgnoreCase))
                    continue;


                string dateTimeSize = link.NextSibling.InnerText.Trim();
                string[] arrAttribs = dateTimeSize.Split(new char[] { ' ', '\t' }, System.StringSplitOptions.RemoveEmptyEntries);
                System.Diagnostics.Debug.WriteLine(arrAttribs);


                string date = arrAttribs[0];
                string time = arrAttribs[1];
                string prettySize = arrAttribs[2];
                string size = prettySize.Replace(",", "");
                string unit = size.Substring(size.Length - 1, 1);
                size = size.Substring(0, size.Length - 1);
                int factor = UnitFactor(unit);
                double d = 0;
                if (double.TryParse(size, out d))
                {
                    d *= factor;
                    d = System.Math.Ceiling(d);
                } // End if (double.TryParse(size, out d))

                ls.Add(new DataDump()
                {
                    Date = date,
                    Time = time,
                    HumanReadableSize = prettySize,
                    Size = (long)d,
                    URL = downloadLink,
                    FileName = att.Value,
                    IsMeta = (att.Value.IndexOf("meta") != -1)
                });

                System.Diagnostics.Debug.WriteLine(d);
                System.Diagnostics.Debug.WriteLine(date + ": " + downloadLink);
            } // Next link 

            ls.Sort(delegate(DataDump a, DataDump b)
            {
                return a.Size.CompareTo(b.Size);
            });

            System.Diagnostics.Debug.WriteLine("Finished ! ");
            return ls;
        } // End Function GetPossibleStackExchangeDataDumps 


    } // End Class DataDumpArchive


} // End Namespace EfficientJsonImporter
