
using System.Xml.Serialization;
using System.Collections.Generic;


namespace Xml2CSharp
{


    [XmlRoot(ElementName = "row")]
    public class Vote : EfficientJsonImporter.TabularData
    {
        [XmlAttribute(AttributeName = "Id")]
        public long Id { get; set; }

        [XmlAttribute(AttributeName = "PostId")]
        public long PostId { get; set; }

        [XmlAttribute(AttributeName = "VoteTypeId")]
        public long VoteTypeId { get; set; }

        [XmlAttribute(AttributeName = "UserId")]
        public long? UserId { get; set; }

        [XmlAttribute(AttributeName = "CreationDate")]
        public System.DateTime? CreationDate { get; set; }

        [XmlAttribute(AttributeName = "BountyAmount")]
        public long? BountyAmount { get; set; }


        public override string FileName
        {
            get
            {
                return "Votes.xml";
            }
        }


        public override void InsertRow(System.Text.StringBuilder sb)
        {
            string strSQL = @"
INSERT INTO Votes( Id, PostId, VoteTypeId, UserId, CreationDate, BountyAmount )
VALUES ({0}, {1}, {2}, {3}, {4}, {5}); 
";

            sb.AppendFormat(strSQL
                , this.Id
                , this.InsertNumber(this.PostId)
                , this.InsertNumber(this.VoteTypeId)
                , this.InsertNumber(this.UserId)
                , this.InsertDate(CreationDate)
                , this.InsertNumber(this.BountyAmount)
            );
        }


    }


    [XmlRoot(ElementName = "votes")]
    public class Votes
    {
        [XmlElement(ElementName = "row")]
        public List<Vote> Row { get; set; }
    }


}
