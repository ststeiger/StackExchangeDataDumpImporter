
using System.Xml.Serialization;
using System.Collections.Generic;


namespace Xml2CSharp
{


    [XmlRoot(ElementName = "row")]
    public class Comment : EfficientJsonImporter.TabularData
    {
        [XmlAttribute(AttributeName = "Id")]
        public long Id { get; set; }

        [XmlAttribute(AttributeName = "PostId")]
        public long PostId { get; set; }

        [XmlAttribute(AttributeName = "Score")]
        public long Score { get; set; }

        [XmlAttribute(AttributeName = "Text")]
        public string Text { get; set; }

        [XmlAttribute(AttributeName = "CreationDate")]
        public System.DateTime CreationDate { get; set; }

        [XmlAttribute(AttributeName = "UserDisplayName")]
        public string UserDisplayName { get; set; }

        [XmlAttribute(AttributeName = "UserId")]
        public long? UserId { get; set; }



        public override string FileName
        {
            get
            {
                return "Comments.xml";
            }
        }


        public override void InsertRow(System.Text.StringBuilder sb)
        {
            string strSQL = @"
INSERT INTO Comments(Id, PostId, Score, Text, CreationDate, UserDisplayName, UserId)
VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6}); 
";

            sb.AppendFormat(strSQL
                , this.Id
                , this.InsertNumber(this.PostId)
                , this.InsertNumber(this.Score)
                , this.InsertString(this.Text)
                , this.InsertDate(this.CreationDate)
                , this.InsertString(this.UserDisplayName)
                , this.InsertNumber(this.UserId)
            );
        }
    }



    [XmlRoot(ElementName = "comments")]
    public class Comments
    {
        [XmlElement(ElementName = "row")]
        public List<Comment> Row { get; set; }
    }


}
