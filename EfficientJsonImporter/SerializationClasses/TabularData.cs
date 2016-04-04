
namespace EfficientJsonImporter
{


    public abstract class TabularData : System.Xml.Serialization.IXmlSerializable
    {
        public TabularData()
        { }


        // public int? Value1 { get; private set; }
        // public float? Value2 { get; private set; }



        public static bool IsNullable(System.Type t)
        {
            if (t == null)
                return false;

            return t.IsGenericType && object.ReferenceEquals(t.GetGenericTypeDefinition(), typeof(System.Nullable<>));
        } // End Function IsNullable


        public static object MyChangeType(object objVal, System.Type t)
        {
            if (objVal == null || object.ReferenceEquals(objVal, System.DBNull.Value))
            {
                return null;
            }

            //getbasetype
            System.Type tThisType = objVal.GetType();

            bool bNullable = IsNullable(t);
            if (bNullable)
            {
                t = System.Nullable.GetUnderlyingType(t);
            }

            if (object.ReferenceEquals(t, typeof(string)) && object.ReferenceEquals(tThisType, typeof(System.Guid)))
            {
                return objVal.ToString();
            }

            if (object.ReferenceEquals(t, typeof(System.Guid)) && object.ReferenceEquals(tThisType, typeof(string)))
            {
                // Target GUID, source: String
                string strUID = System.Convert.ToString(objVal);

                if (bNullable && strUID == null)
                    return null;
                
                return new System.Guid(strUID);
            }


            return System.Convert.ChangeType(objVal, t);
        } // End Function MyChangeType



        public virtual void ReadXml(System.Xml.XmlReader reader)
        {
            System.Type t = this.GetType();
            System.Reflection.PropertyInfo[] pis = t.GetProperties();
            foreach (System.Reflection.PropertyInfo pi in pis)
            {

                // https://stackoverflow.com/questions/6637679/reflection-get-attribute-name-and-value-on-property
                object[] attrs = pi.GetCustomAttributes(true);
                foreach (object attr in attrs)
                {
                    System.Xml.Serialization.XmlAttributeAttribute xmlAttr = attr as System.Xml.Serialization.XmlAttributeAttribute;
                    if (xmlAttr != null)
                    {
                        // string propName = pi.Name;
                        // string attrName = xmlAttr.AttributeName;
                        // System.Console.WriteLine(attrName);

                        string attrValue = reader.GetAttribute(xmlAttr.AttributeName);
                        // System.Console.WriteLine(attrValue);

                        pi.SetValue(this, MyChangeType(attrValue, pi.PropertyType));
                        break;
                    }
                } // Next attr

            } // Next pi


            // string attr1 = reader.GetAttribute("attr1");
            // string attr2 = reader.GetAttribute("attr2");

            // Value1 = ConvertToNullable<int>(attr1);
            // Value2 = ConvertToNullable<float>(attr2);

            reader.Read();
        }

        public System.Xml.Schema.XmlSchema GetSchema() { return null; }

        // https://stackoverflow.com/questions/3250706/xmlserializer-and-nullable-attributes
        public virtual void WriteXml(System.Xml.XmlWriter writer) 
        { 
            throw new System.NotImplementedException(); 
            // if(this.FileName != null) { writer.WriteValue(this.FileName); }
        }


        /*
        private static T? ConvertToNullable<T>(string inputValue) where T : struct
        {
            if (string.IsNullOrEmpty(inputValue) || inputValue.Trim().Length == 0)
            {
                return null;
            }

            try
            {
                System.ComponentModel.TypeConverter conv = System.ComponentModel.TypeDescriptor.GetConverter(typeof(T));
                return (T)conv.ConvertFrom(inputValue);
            }
            catch (System.NotSupportedException ex)
            {
                // The conversion cannot be performed
                System.Console.WriteLine(ex.Message);
                return null;
            }
        }
        */

        public abstract string FileName{ get; }


        public abstract void InsertRow(System.Text.StringBuilder sb);



        public virtual string InsertGuid(System.Guid? uid)
        {
            if(uid.HasValue)
                return "'" + uid.Value.ToString() + "'" ;

            return "NULL";
        }

        public virtual string InsertBit(string str)
        {
            if (str == null)
                return "NULL";

            str = str.Trim();

            if (System.StringComparer.OrdinalIgnoreCase.Equals(str, "true"))
                return "'true'";

            if (System.StringComparer.OrdinalIgnoreCase.Equals(str, "false"))
                return "'false'";

            if (System.StringComparer.OrdinalIgnoreCase.Equals(str, "0"))
                return "'false'";

            return "'true'";
        }


        public virtual string InsertString(string str)
        {
            if (str == null)
                return "NULL";

            return "'" + str.Replace("'", "''") + "'";
        }

        public virtual string InsertNumber(long? num)
        {
            if (num.HasValue)
                return num.Value.ToString(System.Globalization.CultureInfo.InvariantCulture);
            
            return "NULL";
        }

        public virtual string InsertDate(System.DateTime? dt)
        {
            if (dt.HasValue)
                return "'" + dt.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff") + "'";

            return "NULL";
        }
        

    }


}

