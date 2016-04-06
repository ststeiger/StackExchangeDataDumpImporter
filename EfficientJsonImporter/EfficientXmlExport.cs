using System;

namespace EfficientJsonImporter
{
    public class EfficientXmlExport
    {
        public class Account
        {
            
        }


        public static void Test()
        {
            string fn = "/root/Projects/EfficientJsonImporter/EfficientJsonImporter/HTML/foo.xml";
            if(System.IO.File.Exists(fn))
                System.IO.File.Delete(fn);
            
            var sw = new System.IO.StreamWriter(fn);
            SimpleTableSerializer(sw);
        }


        public static void SimpleTableSerializer(System.IO.TextWriter target)
        {

            System.Xml.XmlWriterSettings settings = new System.Xml.XmlWriterSettings(){ 
                    Encoding = System.Text.Encoding.UTF8 
                    ,Indent = true
                    ,IndentChars = "    "
                    ,NewLineChars = "\r\n"
                
            };


            using (System.Xml.XmlWriter writer = System.Xml.XmlWriter.Create(target, settings))
            {
                writer.WriteStartElement("rows");

                using (System.Data.Common.DbDataReader dr = SQL.ExecuteReader("SELECT * FROM badges;", System.Data.CommandBehavior.SequentialAccess))
                {
                    if (dr.HasRows)
                    {
                        int fieldCount = dr.FieldCount;

                        while (dr.Read())
                        {
                            writer.WriteStartElement("row");

                            for (int i = 0; i < fieldCount; ++i)
                            {
                                string colName = dr.GetName(i);
                                object obj = dr.GetValue(i);
                                if (obj != null)
                                {
                                    writer.WriteStartAttribute(colName);
                                    writer.WriteValue(obj);
                                    writer.WriteEndAttribute();

                                    /*
                                    System.Type t = obj.GetType();
                                    if(object.ReferenceEquals(t, typeof(System.DateTime)))
                                    {
                                        System.DateTime dt = (System.DateTime)obj;
                                        dt.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff", System.Globalization.CultureInfo.InvariantCulture);        
                                    }
                                    */
                                } // End if (obj != null)

                            } // Next i

                            writer.WriteEndElement();
                        } // Whend while (dr.Read())

                    } // End if (dr.HasRows)

                } // End using dr 

                writer.WriteEndElement();
            } // End Using writer

        } // End Sub


        public void SerializeData<T>(System.Collections.Generic.IEnumerable<T> data, System.IO.TextWriter target)
        {
            // Use XmlSerializerNamespaces to supress xmlns:xsi and xmlns:xsd
            System.Xml.Serialization.XmlSerializerNamespaces namespaces = new System.Xml.Serialization.XmlSerializerNamespaces();
            namespaces.Add("", "");

            System.Xml.Serialization.XmlSerializer ser = new System.Xml.Serialization.XmlSerializer(typeof(T));

            using (System.Xml.XmlWriter writer = System.Xml.XmlWriter.Create(target))
            {
                writer.WriteStartElement("row");

                foreach (T obj in data)
                {
                    ser.Serialize(writer, obj, namespaces);
                    writer.Flush();
                } // Next T

                writer.WriteEndElement();
                writer.Flush();
            } // End Using writer 

        } // End Sub


    } // End Class 


} // End Namespace 
