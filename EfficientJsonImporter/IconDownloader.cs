
namespace EfficientJsonImporter
{


    public class IconDownloader
    {


        public IconDownloader()
        { }


        public static void DownloadFile(string remoteUri, string fileName)
        {
            // Create a new WebClient instance.
            using (System.Net.WebClient myWebClient = new System.Net.WebClient())
            {
                myWebClient.DownloadFile(remoteUri, fileName);
            }
        } // End Sub DownloadFile 


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


        public static void Test()
        {
            string path = MapProjectPath("HTML/FlatIconMain.txt");
            path = MapProjectPath("HTML/flatIcon_lvl_1.txt");
            path = MapProjectPath("HTML/stack_exchange_data_dumps.txt");

            System.Console.WriteLine(path);
            string harvestPath = MapProjectPath("harvest");
            if (!System.IO.Directory.Exists(harvestPath))
                System.IO.Directory.CreateDirectory(harvestPath);


            string url = "http://www.flaticon.com/packs";
            url = @"http://www.flaticon.com/packs/web-navigation-line-craft";
            url = @"https://archive.org/download/stackexchange";

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
                string[] arrAttribs = dateTimeSize.Split(new char[]{ ' ', '\t' }, System.StringSplitOptions.RemoveEmptyEntries);

                string date = arrAttribs[0];
                string time = arrAttribs[1];
                string size = arrAttribs[2];
                size = size.Replace(",", "");
                string unit = size.Substring(size.Length - 1, 1);
                size = size.Substring(0, size.Length - 1);
                int factor = UnitFactor(unit);
                double d = 0;
                if (double.TryParse(size, out d))
                    d *= factor;
                
                System.Console.WriteLine(d);


                System.Console.WriteLine(arrAttribs);

                System.Console.WriteLine(dateTimeSize +": " + downloadLink);
            }

            System.Console.WriteLine("Finished ! ");


            // <section class="list-top">
            // <a href="http://file005.flaticon.com/packs/112154-web-navigation-line-craft.zip" class="btn pull-right track_download_pack" title="Download Pack" data-pack="112154">Download Pack <i class="flaticon-download"></i></a>

            foreach (HtmlAgilityPack.HtmlNode link in doc.DocumentNode.SelectNodes("//section[@class=\"list-top\"]/a[@href]"))
            {
                System.Console.WriteLine(link);

                HtmlAgilityPack.HtmlAttribute att = link.Attributes["href"];
                string downloadLink = att.Value;

                System.Uri uri = new System.Uri(downloadLink, System.UriKind.Absolute);
                string fn = System.IO.Path.GetFileName(uri.AbsolutePath);
                fn = System.IO.Path.Combine(harvestPath, fn);

                DownloadFile(downloadLink, fn);
                System.Console.WriteLine("Downloaded " + downloadLink + ".");
            }



            //<form name="pagination-form" id="" style="display: block;">
            // <span id="pagination-total">65</span>
            // <a href="http://www.flaticon.com/packs/2" class="pagination-next"></a>

            int iMaxPage = -1;

            foreach (HtmlAgilityPack.HtmlNode span in doc.DocumentNode.SelectNodes("//span[@id=\"pagination-total\"]"))
            {
                bool b = System.Int32.TryParse(span.InnerText, out iMaxPage);
                System.Console.WriteLine(b);
            }



            // http://www.flaticon.com/packs/1
            // http://www.flaticon.com/packs/2
            // ...
            // http://www.flaticon.com/packs/65

            foreach (HtmlAgilityPack.HtmlNode link in doc.DocumentNode.SelectNodes("//article[@class=\"box\"]/a[@href]"))
            {
                HtmlAgilityPack.HtmlAttribute att = link.Attributes["href"];
                System.Console.WriteLine(link);
            } // Next link

        } // End Sub Test


    } // End Class


} 