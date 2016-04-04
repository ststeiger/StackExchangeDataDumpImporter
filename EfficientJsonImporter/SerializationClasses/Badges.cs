
using System.Xml.Serialization;
using System.Collections.Generic;


namespace Xml2CSharp
{


    [XmlRoot(ElementName = "row")]
    public class Badge : EfficientJsonImporter.TabularData
    {
        [XmlAttribute(AttributeName = "Id")]
        public long? Id { get; set; }

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
        public string TagBased { get; set; } // not null



        public override string FileName
        { 
            get{ 
                return "Badges.xml";
            }
        }


        public override void InsertRow(System.Text.StringBuilder sb)
        {
            string strSQL = @"
INSERT INTO badges(Id, UserId, Name, Date, Class, TagBased) 
VALUES ({0}, {1}, {2}, {3}, {4}, {5}); 
";

            sb.AppendFormat(strSQL
                , this.Id
                , this.InsertNumber(this.UserId)
                , this.InsertString(this.Name)
                , this.InsertDate(this.Date)
                , this.InsertNumber(this.Class)
                , this.InsertBit(this.TagBased)
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
