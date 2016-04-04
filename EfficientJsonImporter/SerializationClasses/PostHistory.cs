
using System.Xml.Serialization;
using System.Collections.Generic;


namespace Xml2CSharp
{


    [XmlRoot(ElementName = "row")]
    public class HistoryPost : EfficientJsonImporter.TabularData
    {

        [XmlAttribute(AttributeName = "Id")]
        public long Id { get; set; }

        [XmlAttribute(AttributeName = "PostHistoryTypeId")]
        public long PostHistoryTypeId { get; set; }

        [XmlAttribute(AttributeName = "PostId")]
        public long PostId { get; set; }

        [XmlAttribute(AttributeName = "RevisionGUID")]
        public System.Guid RevisionGUID { get; set; }

        [XmlAttribute(AttributeName = "CreationDate")]
        public System.DateTime CreationDate { get; set; }

        [XmlAttribute(AttributeName = "UserId")]
        public long? UserId { get; set; }

        [XmlAttribute(AttributeName = "UserDisplayName")]
        public string UserDisplayName { get; set; }

        [XmlAttribute(AttributeName = "Comment")]
        public string Comment { get; set; }

        [XmlAttribute(AttributeName = "Text")]
        public string Text { get; set; }


        public override string FileName
        {
            get
            {
                return "PostHistory.xml";
            }
        }


        public override void InsertRow(System.Text.StringBuilder sb)
        {
            string strSQL = @"
INSERT INTO PostHistory( Id, PostHistoryTypeId, PostId, RevisionGUID, CreationDate, UserId, UserDisplayName, Comment, Text )
VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}); 
";

            sb.AppendFormat(strSQL
                , this.Id
                , this.InsertNumber(this.PostHistoryTypeId)
                , this.InsertNumber(this.PostId)
                , this.InsertGuid(this.RevisionGUID)
                , this.InsertDate(this.CreationDate)
                , this.InsertNumber(this.UserId)
                , this.InsertString(this.UserDisplayName)
                , this.InsertString(this.Comment)
                , this.InsertString(this.Text)
            );
        }


    }


    [XmlRoot(ElementName = "posthistory")]
    public class Posthistory
    {
        [XmlElement(ElementName = "row")]
        public List<HistoryPost> Row { get; set; }
    }


}
