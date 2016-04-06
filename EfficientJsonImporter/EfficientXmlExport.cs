
namespace EfficientJsonImporter
{


    public class EfficientXmlExport
    {


        [System.Xml.Serialization.XmlRoot(ElementName = "row")]
        public class Account
        {
            [System.Xml.Serialization.XmlAttribute(AttributeName = "Id")]
            public long Id;

            [System.Xml.Serialization.XmlAttribute(AttributeName = "No")]
            public long No;

            [System.Xml.Serialization.XmlAttribute(AttributeName = "OwnerId")]
            public long OwnerId;
        } // End Class Account 


        private static string MapProjectPath(string path)
        {
            System.Reflection.Assembly ass = System.Reflection.Assembly.GetExecutingAssembly();
            string basePath = System.IO.Path.GetDirectoryName(ass.Location);
            basePath = System.IO.Path.Combine(basePath, "../../");
            path = System.IO.Path.Combine(basePath, path);
            return System.IO.Path.GetFullPath(path);
        } // End Function MapProjectPath 


        public static void Test()
        {
            string fileName = MapProjectPath("ExportDataDump/test.xml");
            SerializeTable("Votes", fileName);
        } // End Sub Test 


        public static void SerializationTest()
        {
            string fileName = MapProjectPath("ExportDataDump/SerializationTest.xml");
            
            System.Collections.Generic.List<Account> data = new System.Collections.Generic.List<Account>();
            data.Add(new Account() { Id = 1, No = 1, OwnerId = 1 });
            data.Add(new Account() { Id = 2, No = 2, OwnerId = 11 });
            data.Add(new Account() { Id = 3, No = 3, OwnerId = 111 });
            data.Add(new Account() { Id = 4, No = 4, OwnerId = 1111 });

            // SerializeData<Account>(null, fileName);
            SerializeData(data, fileName);
        } // End Sub Test 


        public static void SerializeTable(string tableName, string fileName)
        {
            string dir = System.IO.Path.GetDirectoryName(fileName);
            if (!System.IO.Directory.Exists(dir))
                System.IO.Directory.CreateDirectory(dir);

            using (System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.Create, System.IO.FileAccess.Write, System.IO.FileShare.None))
            {
                SerializeTable(tableName, fs);
                fs.Flush();
                fs.Close();
            } // End Using fs

        } // End Sub SerializeTable 


        // public static void SimpleTableSerializer(System.IO.TextWriter target)
        public static void SerializeTable(string tableName, System.IO.Stream target)
        {
            if (string.IsNullOrEmpty(tableName))
                throw new System.ArgumentNullException("tableName is NULL");

            if (null == target)
                throw new System.ArgumentNullException("target is NULL");

            System.Xml.XmlWriterSettings settings = new System.Xml.XmlWriterSettings()
            {
                 Encoding = System.Text.Encoding.UTF8
                ,Indent = true
                ,IndentChars = "  "
                 // Make it Windows-Readable
                ,NewLineChars = "\r\n" // System.Environment.NewLine
            };
            

            using (System.Xml.XmlWriter writer = System.Xml.XmlWriter.Create(target, settings))
            {
                writer.WriteStartElement("rows");

                using (System.Data.Common.DbDataReader dr = SQL.ExecuteReader("SELECT * FROM \"" + tableName.Replace("\"","\"\"") + "\";", System.Data.CommandBehavior.SequentialAccess))
                {
                    if (dr.HasRows)
                    {
                        int fieldCount = dr.FieldCount;

                        while (dr.Read())
                        {
                            writer.WriteStartElement("row");

                            for (int i = 0; i < fieldCount; ++i)
                            {

                                object obj = dr.GetValue(i);
                                if (obj == null)
                                    continue;

                                System.Type t = obj.GetType();
                                if (object.ReferenceEquals(t, typeof(System.DBNull)))
                                    continue;

                                if (object.ReferenceEquals(t, typeof(System.DateTime)))
                                {
                                    System.DateTime dt = (System.DateTime)obj;
                                    obj = dt.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff", System.Globalization.CultureInfo.InvariantCulture);
                                } // End if (object.ReferenceEquals(obj.GetType(), typeof(System.DateTime)))

                                string colName = dr.GetName(i);
                                writer.WriteStartAttribute(colName);
                                writer.WriteValue(obj);
                                writer.WriteEndAttribute();

                            } // Next i

                            writer.WriteEndElement();
                        } // Whend while (dr.Read())

                    } // End if (dr.HasRows)

                } // End using dr 

                writer.WriteEndElement();
            } // End Using writer

        } // End Sub SerializeTable


        public static void SerializeData<T>(System.Collections.Generic.IEnumerable<T> data, string fileName)
        {

            using (System.IO.StreamWriter tw = new System.IO.StreamWriter(fileName, false, System.Text.Encoding.UTF8))
            {
                SerializeData(data, tw);
                tw.Flush();
                tw.Close();
            } // End Using tw

        } // End Sub SerializeData 


        public static void SerializeData<T>(System.Collections.Generic.IEnumerable<T> data, System.IO.TextWriter target)
        {
            if (null == target)
                throw new System.ArgumentNullException("target is NULL");


            System.Xml.XmlWriterSettings settings = new System.Xml.XmlWriterSettings()
            {
                 Encoding = System.Text.Encoding.UTF8
                ,Indent = true
                ,IndentChars = "  "
                // Make it Windows-Readable
                ,NewLineChars = "\r\n" // System.Environment.NewLine
            };

            // Use XmlSerializerNamespaces to supress xmlns:xsi and xmlns:xsd
            System.Xml.Serialization.XmlSerializerNamespaces namespaces = new System.Xml.Serialization.XmlSerializerNamespaces();
            namespaces.Add("", "");

            System.Xml.Serialization.XmlSerializer ser = new System.Xml.Serialization.XmlSerializer(typeof(T));

            using (System.Xml.XmlWriter writer = System.Xml.XmlWriter.Create(target, settings))
            {
                writer.WriteStartElement("rows");

                if (data != null)
                {
                    foreach (T obj in data)
                    {
                        ser.Serialize(writer, obj, namespaces);
                        writer.Flush();
                    } // Next T
                }

                writer.WriteEndElement();
                writer.Flush();
            } // End Using writer 

        } // End Sub SerializeData 


    } // End Class EfficientXmlExport 


} // End Namespace EfficientJsonImporter 
