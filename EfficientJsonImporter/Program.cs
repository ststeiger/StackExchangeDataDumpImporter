
namespace EfficientJsonImporter
{


    class MainClass
    {


        public static void Main(string[] args)
        {
            System.Console.WriteLine(" You can add your database in connections.config");
            System.Console.WriteLine(" Attribute \"name\" is Environment.MachineName)");
            System.Console.WriteLine(" Non-MS databases need the factory type namespace in the provider attribute");
            System.Console.WriteLine(" e.g. typeof(Npgsql.NpgsqlFactory).Namespace ==> Npgsql");
            System.Console.WriteLine(" you'll need to add the assembly to the sourcecode, except for Npgsql, because that assembly is already referenced...");
            System.Console.WriteLine(" Mapping the assembly in App.config is NOT necessary (uses reflection)");
            System.Console.WriteLine(System.Environment.NewLine);


            System.Collections.Generic.List<DataDump> ls = DataDumpArchive.GetPossibleStackExchangeDataDumps();
            System.Console.WriteLine(ls.Count);

            // SevenZip.TestExtract();

            // EfficientXmlImport.HorribleFirstAttempt();
            // EfficientXmlImport.Test();

            EfficientXmlExport.Test();
            EfficientXmlExport.SerializationTest();

            System.Console.WriteLine(" --- Press any key to continue --- ");
            System.Console.ReadKey();
        } // End Sub Main 


    } // End Class MainClass


} // End Namespace EfficientJsonImporter 
