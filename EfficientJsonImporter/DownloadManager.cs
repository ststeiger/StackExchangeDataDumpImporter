using System;

namespace EfficientJsonImporter
{
    public class DownloadManager
    {
        public DownloadManager()
        {
        }


        private void AsyncDownloadTest()
        {
            using (System.Net.WebClient wc = new System.Net.WebClient())
            {
                wc.DownloadProgressChanged += cb_DownloadProgressChanged;
                wc.DownloadFileAsync(new System.Uri("http://www.sayka.in/downloads/front_view.jpg"), "D:\\Images\\front_view.jpg");
            }
        }

        void cb_DownloadProgressChanged(object sender, System.Net.DownloadProgressChangedEventArgs e)
        {
            // progressBar.Value = e.ProgressPercentage;
        }



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


            byte[] ba = System.IO.File.ReadAllBytes("partial");
            long BytesRead = ba.LongLength;
            bool resuming = false;

            //HttpWebRequest hrequest = (HttpWebRequest)request;
            //hrequest.AddRange(BytesRead); ::TODO: Work on this
            if (BytesRead > 0)
            {
                resuming = true;
                request.AddRange(BytesRead);
            }

            System.Net.WebResponse response = request.GetResponse();

            //result.MimeType = response.ContentType;
            //result.LastModified = response.LastModified;
            if (!resuming)//(Size == 0)
            {
                //resuming = false;
                int Size = (int)response.ContentLength;
                int SizeInKB = (int)Size / 1024;
            }

            bool acceptRanges = string.Compare(response.Headers["Accept-Ranges"], "bytes", true) == 0;

            // Create network stream
            System.IO.Stream ns = response.GetResponseStream();
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




    }
}

