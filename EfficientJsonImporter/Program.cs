
namespace EfficientJsonImporter
{


    class MainClass
    {


        public static void Main(string[] args)
        {
            System.Collections.Generic.List<DataDump> ls = DataDumpArchive.GetPossibleStackOverflowDataDumps();
            // EfficientXmlImport.Test();
            // EfficientXmlImport.EfficientTest();

            System.Console.WriteLine(ls);

            System.Console.WriteLine(" --- Press any key to continue --- ");
            System.Console.ReadKey();
        } // End Sub Main 


    } // End Class


} // End Namespace EfficientJsonImporter 
