
using System.Xml.Serialization;
using System.Collections.Generic;


namespace EfficientJsonImporter.Xml2CSharp
{


    [XmlRoot(ElementName = "row")]
    public class Badge : TabularData
    {
        [XmlAttribute(AttributeName = "Id")]
        public long Id { get; set; }

        [XmlAttribute(AttributeName = "UserId")]
        public long UserId { get; set; }

        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "Date")]
        public System.DateTime Date { get; set; }

        [XmlAttribute(AttributeName = "Class")]
        public long Class { get; set; }

        [XmlAttribute(AttributeName = "TagBased")]
        //public bool TagBased { get; set; }
        public string TagBased { get; set; }



        public override string FileName
        { 
            get{ 
                return "Badges.xml";
            }
        }


        public override void InsertRow(System.Text.StringBuilder sb)
        {
            string strSQL =@"
    INSERT INTO badges(id, userid, name, date) 
    VALUES ({0}, {1}, {2}, {3}); 
    ";

            sb.AppendFormat(strSQL
                ,this.Id
                ,this.UserId
                ,this.Name != null ? "'" + this.Name.Replace("'","''") + "'" : "NULL"
                ,this.Date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff")
            );
        }

    }


    [XmlRoot(ElementName = "badges")]
    public class Badges
    {
        [XmlElement(ElementName = "row")]
        public List<Badge> Row { get; set; }
    }


} // End Nnamespace EfficientJsonImporter.Xml2CSharp 
