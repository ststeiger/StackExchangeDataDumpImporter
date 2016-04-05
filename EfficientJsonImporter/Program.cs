
namespace EfficientJsonImporter
{


    class MainClass
    {


        public static void Main(string[] args)
        {
            System.Collections.Generic.List<DataDump> ls = DataDumpArchive.GetPossibleStackExchangeDataDumps();
            // EfficientXmlImport.HorribleFirstAttempt();

            string fn = @"D:\username\Documents\Downloads\startups.stackexchange.com.7z";
            if(System.Environment.OSVersion.Platform == System.PlatformID.Unix)
                fn = @"/root/Downloads/startups.stackexchange.com.7z";

            SevenZip.ExtractFile(fn);

            // EfficientXmlImport.Test();

            System.Console.WriteLine(ls);

            System.Console.WriteLine(" --- Press any key to continue --- ");
            System.Console.ReadKey();
        } // End Sub Main 


    } // End Class MainClass


} // End Namespace EfficientJsonImporter 
