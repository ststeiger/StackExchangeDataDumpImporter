
namespace EfficientJsonImporter
{


    public class EfficientJsonHandling
    {


        public class Row
        {
            public string col1;
            public int col2;
        } // End Class Row 


        private static string MapProjectPath(string path)
        {
            System.Reflection.Assembly ass = System.Reflection.Assembly.GetExecutingAssembly();
            string basePath = System.IO.Path.GetDirectoryName(ass.Location);
            basePath = System.IO.Path.Combine(basePath, "../../");
            path = System.IO.Path.Combine(basePath, path);
            return System.IO.Path.GetFullPath(path);
        } // End Function MapProjectPath 


        public static void TestTableSerialization()
        {
            string tableName = "Votes";
            string fn = MapProjectPath("JsonDump/" + tableName + ".txt");
            string dir = System.IO.Path.GetDirectoryName(fn);
            if (!System.IO.Directory.Exists(dir))
                System.IO.Directory.CreateDirectory(dir);

            SerializeTable(fn, tableName);
        } // End Sub TestTableSerialization


        public static void TestSerialize()
        {
            string fn = MapProjectPath("JsonDump/SerializationTest.json.txt");
            string dir = System.IO.Path.GetDirectoryName(fn);
            if (!System.IO.Directory.Exists(dir))
                System.IO.Directory.CreateDirectory(dir);

            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Columns.Add("col1", typeof(string));
            dt.Columns.Add("col2", typeof(int));

            for (int i = 0; i < 10; ++i)
            {
                System.Data.DataRow dr = dt.NewRow();
                dr["col1"] = i.ToString();
                dr["col2"] = i.ToString();

                dt.Rows.Add(dr);
            } // Next i 
            
            bool bUseManualTable = true;

            using (System.IO.FileStream fs = new System.IO.FileStream(fn, System.IO.FileMode.Create, System.IO.FileAccess.Write, System.IO.FileShare.None))
            {
                if (bUseManualTable)
                {
                    // http://www.newtonsoft.com/json/help/html/readingwritingjson.htm

                    WriteRaw("[", fs); // writer.WriteStartArray();
                    for (int i = 0; i < 10; ++i)
                    {
                        bool bSimpleSerialization = true;
                        if (bSimpleSerialization)
                        {
                            Serialize(new Row() { col1 = i.ToString(), col2 = i }, fs);

                            // TODO: Test if last entry
                            WriteRaw(",", fs);
                        } // End if (bSimpleSerialization)
                        else
                        {
                            WriteRaw("{", fs); // jsonWriter.WriteStartObject();

                            WriteKeyValue("col1", i.ToString(), fs);
                            WriteRaw(",", fs);
                            WriteKeyValue("col2", i, fs);
                            // WriteRaw(",", fs);

                            WriteRaw("}", fs); // jsonWriter.WriteEndObject();
                            WriteRaw(",", fs);
                        } // End else of if (bSimpleSerialization)

                    } // Next i 
                    WriteRaw("]", fs); // writer.WriteEnd();
                } // End if (bUseManualTable)
                else
                    Serialize(dt, fs);

                
                fs.Flush();
                fs.Close();
            } // End Using fs 

        } // End Sub TestSerialize 


        public static void SerializeTable(string fileName, string tableName)
        {
            bool bOmitNullValues = false;
            bool bPrettyPrint = true;
            Newtonsoft.Json.JsonSerializer ser = new Newtonsoft.Json.JsonSerializer();

            using (System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.Create, System.IO.FileAccess.Write, System.IO.FileShare.None))
            {

                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(fs, System.Text.Encoding.UTF8))
                {
                    using (Newtonsoft.Json.JsonTextWriter jsonWriter = new Newtonsoft.Json.JsonTextWriter(sw))
                    {
                        if (bPrettyPrint)
                            jsonWriter.Formatting = Newtonsoft.Json.Formatting.Indented;
                        else
                            jsonWriter.Formatting = Newtonsoft.Json.Formatting.None;

                        jsonWriter.WriteStartArray();

                        using (System.Data.Common.DbDataReader dr = SQL.ExecuteReader("SELECT * FROM \"" + tableName.Replace("\"", "\"\"") + "\";", System.Data.CommandBehavior.SequentialAccess))
                        {
                            if (dr.HasRows)
                            {
                                int fieldCount = dr.FieldCount;

                                while (dr.Read())
                                {
                                    jsonWriter.WriteStartObject();

                                    for (int i = 0; i < fieldCount; ++i)
                                    {

                                        object obj = dr.GetValue(i);
                                        if (obj == null)
                                            continue;

                                        System.Type t = obj.GetType();

                                        if (bOmitNullValues)
                                        {
                                            if (object.ReferenceEquals(t, typeof(System.DBNull)))
                                                continue;
                                        } // End if (bOmitNullValues)

                                        if (object.ReferenceEquals(t, typeof(System.DateTime)))
                                        {
                                            System.DateTime dt = (System.DateTime)obj;
                                            obj = dt.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff", System.Globalization.CultureInfo.InvariantCulture);
                                        } // End if (object.ReferenceEquals(obj.GetType(), typeof(System.DateTime)))

                                        string colName = dr.GetName(i);
                                        jsonWriter.WritePropertyName(colName);
                                        jsonWriter.WriteValue(obj);
                                    } // Next i

                                    jsonWriter.WriteEndObject();
                                } // Whend while (dr.Read())

                            } // End if (dr.HasRows)

                        } // End using dr 

                        jsonWriter.WriteEnd(); // WriteRaw("]", fs);
                    } // End Using jsonWriter 

                } // End Using  sw

            } // End Using fs

        } // End Sub SerializeTable


        public static void WriteRaw(string str, System.IO.Stream s)
        {
            System.IO.StreamWriter writer = new System.IO.StreamWriter(s, System.Text.Encoding.UTF8);
            Newtonsoft.Json.JsonTextWriter jsonWriter = new Newtonsoft.Json.JsonTextWriter(writer);
            
            jsonWriter.WriteRaw(str);
            jsonWriter.Flush();
        } // End Sub WriteRaw 


        public static void WriteKeyValue(string key, object value, System.IO.Stream s)
        {
            System.IO.StreamWriter writer = new System.IO.StreamWriter(s, System.Text.Encoding.UTF8);
            Newtonsoft.Json.JsonTextWriter jsonWriter = new Newtonsoft.Json.JsonTextWriter(writer);

            jsonWriter.WritePropertyName(key);
            jsonWriter.WriteValue(value);
            // jsonWriter.WriteRaw(",");

            jsonWriter.Flush();
        } // End Sub WriteKeyValue 


        public static void Serialize(object value, System.IO.Stream s)
        {
            System.IO.StreamWriter writer = new System.IO.StreamWriter(s, System.Text.Encoding.UTF8);
            Newtonsoft.Json.JsonTextWriter jsonWriter = new Newtonsoft.Json.JsonTextWriter(writer);
            Newtonsoft.Json.JsonSerializer ser = new Newtonsoft.Json.JsonSerializer();
            ser.Serialize(jsonWriter, value);
            jsonWriter.Flush();
        } // End Sub Serialize


        public static void TestDeserialize()
        {
            string fn = MapProjectPath("JsonDump/SerializationTest.json.txt");
            DeserializeTable(fn);
        } // End Sub TestDeserialize 


        public static void DeserializeTable(string fileName)
        {
            Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();

            using (System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read))
            {

                using (System.IO.StreamReader sr = new System.IO.StreamReader(fs, System.Text.Encoding.UTF8))
                {

                    using (Newtonsoft.Json.JsonTextReader reader = new Newtonsoft.Json.JsonTextReader(sr))
                    {
                        while (reader.Read())
                        {

                            if (reader.TokenType == Newtonsoft.Json.JsonToken.StartObject)
                            {
                                bool bSimple = false;

                                if (bSimple)
                                {
                                    // Variant1: No Serialization Class required
                                    // Load each object from the stream and do something with it
                                    Newtonsoft.Json.Linq.JObject obj = Newtonsoft.Json.Linq.JObject.Load(reader);
                                    System.Console.WriteLine(obj);
                                    // System.Console.WriteLine(obj["col1"] + " - " + obj["col2"]);
                                    foreach (Newtonsoft.Json.Linq.JProperty prop in obj.Properties())
                                    {
                                        System.Console.WriteLine("  - " + prop.Name + ": " + prop.Value);
                                    } // Next prop 

                                } // End if (bSimple)
                                else
                                {
                                    // Variant2: Serialization Class required
                                    // https://stackoverflow.com/questions/20374083/deserialize-json-array-stream-one-item-at-a-time
                                    // serializer.Deserialize(reader, typeof(Row));
                                    Row thisRow = serializer.Deserialize<Row>(reader);
                                    System.Console.WriteLine(thisRow);
                                    System.Console.WriteLine("  - col1: " + thisRow.col1);
                                    System.Console.WriteLine("  - col2: " + thisRow.col2);
                                } // End else of if (bSimple)

                            } // End if (reader.TokenType == JsonToken.StartObject) 

                        } // Whend

                    } // End Using reader

                } // End using sr

            } // End Using fs

        } // End Sub DeserializeTable


    } // End class EfficientJsonHandling


} // End Namespace EfficientJsonImporter
