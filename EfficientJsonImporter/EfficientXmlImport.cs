
namespace EfficientJsonImporter
{


    public class EfficientXmlImport
    {


        public EfficientXmlImport()
        { }


        public static string GetConnectionString()
        {
            string str = @"CREATE ROLE stackexchangeimporter LOGIN PASSWORD '123' 
SUPERUSER INHERIT CREATEDB CREATEROLE REPLICATION;
";

            Npgsql.NpgsqlConnectionStringBuilder csb = new Npgsql.NpgsqlConnectionStringBuilder();
            csb.UserName = "stackexchangeimporter";
            csb.Password = "123";
            csb.Port = 5432;    
            csb.Database = "startups";
            csb.Host = "127.0.0.1";

            return csb.ToString();
        } // End Function GetConnectionString



        public static void EfficientTest()
        {
            Parse<EfficientJsonImporter.Xml2CSharp.Badge>();
        }


        public static void Parse<T>()  where T: TabularData
        {
            Parse<T>(270);
        }

        public static void Parse<T>(int batchSize)  where T: TabularData
        {
            string fileName = @"/root/Downloads/startups.stackexchange.com/";

            TabularData tdFile = (TabularData) (object) System.Activator.CreateInstance<T>();
            fileName = System.IO.Path.Combine(fileName, tdFile.FileName);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            // Parse...
            using (System.Xml.XmlReader xmlReader = System.Xml.XmlReader.Create(fileName))
            {
                System.Xml.Serialization.XmlSerializer RowSerializer = new System.Xml.Serialization.XmlSerializer(typeof(T));

                int iRowCounter = 0;

                // Parse XML - "row" nodes...
                while (xmlReader.ReadToFollowing("row"))
                {
                    TabularData td = (T) RowSerializer.Deserialize(xmlReader.ReadSubtree());
                    td.InsertRow(sb);
                    // rows.Add(rowObject);

                    ++iRowCounter;
                    if(iRowCounter % batchSize == 0)
                    {
                        string str = sb.ToString();
                        System.Console.WriteLine(str);
                        sb.Length = 0;
                    }
                }

                if(sb.Length > 0)
                {
                    System.Console.WriteLine(sb.ToString());
                    sb.Length = 0;
                }

                // Cleanup...
                xmlReader.Close();
            }

        }

        public static void Test()
        {
            string strSQL = @"SELECT * FROM information_schema.columns WHERE lower(table_name) = lower(@__colname)";

            string fileName = @"/root/Downloads/startups.stackexchange.com/Badges.xml";

            using (System.Xml.XmlReader reader = System.Xml.XmlReader.Create(fileName))
            {
                reader.MoveToContent(); // will not advance reader if already on a content node; if successful, ReadState is Interactive
                reader.Read();          // this is needed, even with MoveToContent and ReadState.Interactive

                while(!reader.EOF && reader.ReadState == System.Xml.ReadState.Interactive)
                {
                   
                    // corrected for bug noted by Wes below...
                    if(reader.NodeType == System.Xml.XmlNodeType.Element && reader.Name.Equals("row"))
                    {
                        
                        if(reader.HasAttributes)
                        {
                            while (reader.MoveToNextAttribute())
                            {
                                System.Console.WriteLine(reader.Name);
                                System.Console.WriteLine(reader.ValueType);
                                System.Console.WriteLine(reader.Value);
                                System.DateTime dt;
                                bool b = System.DateTime.TryParseExact(reader.Value, "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dt);
                                System.Console.WriteLine(b);
                            }

                        } // End if(reader.HasAttributes)
                    }
                    else
                        reader.Read();
                } // Whend

            } // End Using reader 

        } // End Sub Test 


    } // End Class EfficientXmlImport


} // End Namespace EfficientJsonImporter 
