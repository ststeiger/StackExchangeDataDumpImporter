
using System.Xml.Serialization;
using System.Collections.Generic;


namespace Xml2CSharp
{


    [XmlRoot(ElementName = "row")]
    public class Tag : EfficientJsonImporter.TabularData
    {
        [XmlAttribute(AttributeName = "Id")]
        public long Id { get; set; }

        [XmlAttribute(AttributeName = "TagName")]
        public string TagName { get; set; }

        [XmlAttribute(AttributeName = "Count")]
        public long Count { get; set; }

        [XmlAttribute(AttributeName = "ExcerptPostId")]
        public long? ExcerptPostId { get; set; }

        [XmlAttribute(AttributeName = "WikiPostId")]
        public long? WikiPostId { get; set; }


        public override string FileName
        {
            get
            {
                return "Tags.xml";
            }
        }


        public override void InsertRow(System.Text.StringBuilder sb)
        {
            string strSQL = @"
INSERT INTO Tags( Id, TagName, Count, ExcerptPostId, WikiPostId )
VALUES ({0}, {1}, {2}, {3}, {4}); 
";

            sb.AppendFormat(strSQL
                , this.Id
                , this.InsertString(this.TagName)
                , this.InsertNumber(this.Count)
                , this.InsertNumber(this.ExcerptPostId)
                , this.InsertNumber(this.WikiPostId)
            );
        }

    }


    [XmlRoot(ElementName = "tags")]
    public class Tags
    {
        [XmlElement(ElementName = "row")]
        public List<Tag> Row { get; set; }
    }


}
