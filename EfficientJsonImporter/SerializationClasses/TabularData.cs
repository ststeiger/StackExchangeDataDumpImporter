
namespace EfficientJsonImporter
{


    public abstract class TabularData : System.Xml.Serialization.IXmlSerializable
    {


        private static bool IsNullable(System.Type t)
        {
            if (t == null)
                return false;

            return t.IsGenericType && object.ReferenceEquals(t.GetGenericTypeDefinition(), typeof(System.Nullable<>));
        } // End Function IsNullable


        private static object NullableCapableChangeType(object objVal, System.Type t)
        {
            if (objVal == null || object.ReferenceEquals(objVal, System.DBNull.Value))
            {
                return null;
            } // End if (objVal == null || object.ReferenceEquals(objVal, System.DBNull.Value)) 

            //getbasetype
            System.Type tThisType = objVal.GetType();

            bool bNullable = IsNullable(t);
            if (bNullable)
            {
                t = System.Nullable.GetUnderlyingType(t);
            } // End if (bNullable)

            if (object.ReferenceEquals(t, typeof(string)) && object.ReferenceEquals(tThisType, typeof(System.Guid)))
            {
                return objVal.ToString();
            } // End if (object.ReferenceEquals(t, typeof(string)) && object.ReferenceEquals(tThisType, typeof(System.Guid))) 

            if (object.ReferenceEquals(t, typeof(System.Guid)) && object.ReferenceEquals(tThisType, typeof(string)))
            {
                // Target GUID, source: String
                string strUID = System.Convert.ToString(objVal);

                if (bNullable && strUID == null)
                    return null;
                
                return new System.Guid(strUID);
            } // End if (object.ReferenceEquals(t, typeof(System.Guid)) && object.ReferenceEquals(tThisType, typeof(string))) 

            return System.Convert.ChangeType(objVal, t);
        } // End Function NullableCapableChangeType


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

                        pi.SetValue(this, NullableCapableChangeType(attrValue, pi.PropertyType));
                        break;
                    }
                } // Next attr

            } // Next pi

            reader.Read();
        } // End Sub ReadXml 


        public virtual System.Xml.Schema.XmlSchema GetSchema() { return null; }

        // https://stackoverflow.com/questions/3250706/xmlserializer-and-nullable-attributes
        public virtual void WriteXml(System.Xml.XmlWriter writer) 
        { 
            throw new System.NotImplementedException(); 
            // if(this.FileName != null) { writer.WriteValue(this.FileName); }
        } // End Sub WriteXml 


        public abstract string FileName{ get; }

        public abstract void InsertRow(System.Text.StringBuilder sb);



        public virtual string InsertGuid(System.Guid? uid)
        {
            if(uid.HasValue)
                return "'" + uid.Value.ToString() + "'" ;

            return "NULL";
        } // End Function InsertGuid


        public virtual string InsertBit(string str)
        {
            if (str == null)
                return "NULL";

            str = str.Trim();

            if (System.StringComparer.OrdinalIgnoreCase.Equals(str, "true"))
                return "'true'";

            if (System.StringComparer.OrdinalIgnoreCase.Equals(str, "false"))
                return "'false'";
            
            if (System.StringComparer.OrdinalIgnoreCase.Equals(str, "yes"))
                return "'true'";

            if (System.StringComparer.OrdinalIgnoreCase.Equals(str, "no"))
                return "'false'";

            if (System.StringComparer.OrdinalIgnoreCase.Equals(str, "0"))
                return "'false'";
            
            return "'true'";
        } // End Function InsertBit


        public virtual string InsertString(string str)
        {
            if (str == null)
                return "NULL";

            return "'" + str.Replace("'", "''") + "'";
        } // End Function InsertString


        public virtual string InsertNumber(long? num)
        {
            if (num.HasValue)
                return num.Value.ToString(System.Globalization.CultureInfo.InvariantCulture);
            
            return "NULL";
        } // End Function InsertNumber 


        public virtual string InsertDate(System.DateTime? dt)
        {
            if (dt.HasValue)
                return "'" + dt.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff") + "'";

            return "NULL";
        } // End Function InsertDate 


    } // End abstract class TabularData : System.Xml.Serialization.IXmlSerializable


} // End Namespace EfficientJsonImporter
