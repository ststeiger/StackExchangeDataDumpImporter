using System;

namespace EfficientJsonImporter
{
    class MainClass
    {


        public static void ResumeRetry(string Url)
        {
            bool UseProxy = true;
            string ProxyServer = "";
            int ProxyPort = 8080;
            string ProxyUsername = "";
            string ProxyPassword = "";


            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(Url);
            if (UseProxy)
            {
                request.Proxy = new System.Net.WebProxy(ProxyServer + ":" + ProxyPort.ToString());
                if (ProxyUsername.Length > 0)
                    request.Proxy.Credentials = new System.Net.NetworkCredential(ProxyUsername, ProxyPassword);
            }
            /*
            //HttpWebRequest hrequest = (HttpWebRequest)request;
            //hrequest.AddRange(BytesRead); ::TODO: Work on this
            if (BytesRead > 0) request.AddRange(BytesRead);

            System.Net.WebResponse response = request.GetResponse();
            //result.MimeType = res.ContentType;
            //result.LastModified = response.LastModified;
            if (!resuming)//(Size == 0)
            {
                //resuming = false;
                Size = (int)response.ContentLength;
                SizeInKB = (int)Size / 1024;
            }
            acceptRanges = String.Compare(response.Headers["Accept-Ranges"], "bytes", true) == 0;

            //create network stream
            ns = response.GetResponseStream();
             */
        }


        static void DownloadFileWithResume(string sSourceURL, string sDestinationPath)
        {
            long iFileSize = 0;
            int iBufferSize = 1024;
            iBufferSize *= 1000;
            long iExistLen = 0;

            System.IO.FileStream saveFileStream;
            if (System.IO.File.Exists(sDestinationPath))
            {
                System.IO.FileInfo fINfo = new System.IO.FileInfo(sDestinationPath);
                iExistLen = fINfo.Length;
            }

            if (iExistLen > 0)
                saveFileStream = new System.IO.FileStream(sDestinationPath, System.IO.FileMode.Append, System.IO.FileAccess.Write, System.IO.FileShare.ReadWrite);
            else
                saveFileStream = new System.IO.FileStream(sDestinationPath, System.IO.FileMode.Create, System.IO.FileAccess.Write, System.IO.FileShare.ReadWrite);

            System.Net.HttpWebRequest hwRq;
            System.Net.HttpWebResponse hwRes;
            hwRq = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(sSourceURL);
            hwRq.AddRange((int)iExistLen);
            System.IO.Stream smRespStream;
            hwRes = (System.Net.HttpWebResponse)hwRq.GetResponse();
            smRespStream = hwRes.GetResponseStream();

            iFileSize = hwRes.ContentLength;

            int iByteSize;
            byte[] downBuffer = new byte[iBufferSize];

            while ((iByteSize = smRespStream.Read(downBuffer, 0, downBuffer.Length)) > 0)
            {
                saveFileStream.Write(downBuffer, 0, iByteSize);
            }
        }  

        public static void DownloadFile()
        {
            string remoteUri = "http://www.contoso.com/library/homepage/images/";
            string fileName = "ms-banner.gif";

            // Create a new WebClient instance.
            using (System.Net.WebClient myWebClient = new System.Net.WebClient())
            {
                // myWebClient.DownloadFile("http://csharpindepth.com/About.aspx", @"c:\Users\Jon\Test\foo.txt");

                string myStringWebResource = remoteUri + fileName;
                // Download the Web resource and save it into the current filesystem folder.
                myWebClient.DownloadFile(myStringWebResource, fileName);
            }
        }

        private void BtnDownload_Click()
        {
            using (System.Net.WebClient wc = new System.Net.WebClient())
            {
                wc.DownloadProgressChanged += wc_DownloadProgressChanged;
                wc.DownloadFileAsync(new System.Uri("http://www.sayka.in/downloads/front_view.jpg"), "D:\\Images\\front_view.jpg");
            }
        }

        void wc_DownloadProgressChanged(object sender, System.Net.DownloadProgressChangedEventArgs e)
        {
            // progressBar.Value = e.ProgressPercentage;
        }



        public static void Test()
        {
            string url = "http://www.flaticon.com/packs";
            url = @"http://www.flaticon.com/packs/web-navigation-line-craft";

            HtmlAgilityPack.HtmlWeb page = new HtmlAgilityPack.HtmlWeb();
            // HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            // doc.Load(path);
            HtmlAgilityPack.HtmlDocument doc = page.Load(url);

            // System.Console.WriteLine(doc.DocumentNode.OuterHtml);





            // <section class="list-top">
		    // <a href="http://file005.flaticon.com/packs/112154-web-navigation-line-craft.zip" class="btn pull-right track_download_pack" title="Download Pack" data-pack="112154">Download Pack <i class="flaticon-download"></i></a>

            foreach (HtmlAgilityPack.HtmlNode link in doc.DocumentNode.SelectNodes("//section[@class=\"list-top\"]/a[@href]"))
            {
                HtmlAgilityPack.HtmlAttribute att = link.Attributes["href"];
                string downloadLink = att.Value;

                System.Console.WriteLine(link);
            }







            //<form name="pagination-form" id="" style="display: block;">
            // <span id="pagination-total">65</span>
            // <a href="http://www.flaticon.com/packs/2" class="pagination-next"></a>




            string pageMax = null;

            foreach (HtmlAgilityPack.HtmlNode span in doc.DocumentNode.SelectNodes("//span[@id=\"pagination-total\"]"))
            {
                pageMax = span.InnerText;
            }



            // http://www.flaticon.com/packs/1
            // http://www.flaticon.com/packs/2
            // ...
            // http://www.flaticon.com/packs/65

            foreach (HtmlAgilityPack.HtmlNode link in doc.DocumentNode.SelectNodes("//article[@class=\"box\"]/a[@href]"))
            {
                HtmlAgilityPack.HtmlAttribute att = link.Attributes["href"];
                System.Console.WriteLine(link);
            }


        }


        public static void Main(string[] args)
        {
            Test();
            // EfficientXmlImport.Test();
            // EfficientXmlImport.EfficientTest();
            Console.WriteLine("Hello World!");
        }
    }
}
