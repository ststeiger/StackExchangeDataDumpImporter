
using System.Xml.Serialization;
using System.Collections.Generic;


namespace Xml2CSharp
{


    [XmlRoot(ElementName = "row")]
    public class Post : EfficientJsonImporter.TabularData
    {

        [XmlAttribute(AttributeName = "Id")]
        public long Id { get; set; }

        [XmlAttribute(AttributeName = "PostTypeId")]
        public long PostTypeId { get; set; }

        [XmlAttribute(AttributeName = "AcceptedAnswerId")]
        public long? AcceptedAnswerId { get; set; }


        //[XmlIgnore]
        //public long? m_AcceptedAnswerId { get; set; }
        //[XmlAttribute(AttributeName = "AcceptedAnswerId")]
        //public long AcceptedAnswerId { get { return m_AcceptedAnswerId.Value; } set { m_AcceptedAnswerId = value; } }


        [XmlAttribute(AttributeName = "ParentId")]
        public long? ParentId { get; set; }

        [XmlAttribute(AttributeName = "CreationDate")]
        public System.DateTime CreationDate { get; set; }

        [XmlAttribute(AttributeName = "DeletionDate")]
        public System.DateTime? DeletionDate { get; set; }

        [XmlAttribute(AttributeName = "Score")]
        public long? Score { get; set; }

        [XmlAttribute(AttributeName = "ViewCount")]
        public long? ViewCount { get; set; }

        [XmlAttribute(AttributeName = "Body")]
        public string Body { get; set; }

        [XmlAttribute(AttributeName = "OwnerUserId")]
        public long? OwnerUserId { get; set; }

        [XmlAttribute(AttributeName = "OwnerDisplayName")]
        public string OwnerDisplayName { get; set; }

        [XmlAttribute(AttributeName = "LastEditorUserId")]
        public long? LastEditorUserId { get; set; }

        [XmlAttribute(AttributeName = "LastEditorDisplayName")]
        public string LastEditorDisplayName { get; set; }

        [XmlAttribute(AttributeName = "LastEditDate")]
        public System.DateTime? LastEditDate { get; set; }

        [XmlAttribute(AttributeName = "LastActivityDate")]
        public System.DateTime? LastActivityDate { get; set; }

        [XmlAttribute(AttributeName = "Title")]
        public string Title { get; set; }

        [XmlAttribute(AttributeName = "Tags")]
        public string Tags { get; set; }

        [XmlAttribute(AttributeName = "AnswerCount")]
        public long? AnswerCount { get; set; }
        
        [XmlAttribute(AttributeName = "CommentCount")]
        public long? CommentCount { get; set; }

        [XmlAttribute(AttributeName = "FavoriteCount")]
        public long? FavoriteCount { get; set; }
        
        [XmlAttribute(AttributeName = "ClosedDate")]
        public System.DateTime? ClosedDate { get; set; }

        [XmlAttribute(AttributeName = "CommunityOwnedDate")]
        public System.DateTime? CommunityOwnedDate { get; set; }


        // [XmlIgnore]
        // public System.DateTime? m_CommunityOwnedDate { get; set; }
        // [XmlAttribute(AttributeName = "CommunityOwnedDate")]
        // public System.DateTime CommunityOwnedDate { get { return m_CommunityOwnedDate.Value; } set { m_CommunityOwnedDate = value; } }


        public override string FileName
        {
            get
            {
                return "Posts.xml";
            }
        }


        public override void InsertRow(System.Text.StringBuilder sb)
        {
            string strSQL = @"
INSERT INTO Posts
(
	 Id, PostTypeId, AcceptedAnswerId, ParentId
	,CreationDate, DeletionDate
	,Score, ViewCount, Body, OwnerUserId, OwnerDisplayName
	,LastEditorUserId, LastEditorDisplayName, LastEditDate, LastActivityDate
	,Title, Tags
	,AnswerCount, CommentCount, FavoriteCount
	,ClosedDate, CommunityOwnedDate
)
VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}
,{11}, {12}, {13}, {14}, {15}, {16}, {17}, {18}, {19}, {20}, {21}
); 
";

            sb.AppendFormat(strSQL
                , this.Id
                , this.InsertNumber(this.PostTypeId)
                , this.InsertNumber(this.AcceptedAnswerId)
                , this.InsertNumber(this.ParentId)
                , this.InsertDate(this.CreationDate)
                , this.InsertDate(this.DeletionDate)
                , this.InsertNumber(this.Score)
                , this.InsertNumber(this.ViewCount)
                , this.InsertString(this.Body)
                , this.InsertNumber(this.OwnerUserId)
                , this.InsertString(this.OwnerDisplayName)
                , this.InsertNumber(this.LastEditorUserId)
                , this.InsertString(this.LastEditorDisplayName)
                , this.InsertDate(this.LastEditDate)
                , this.InsertDate(this.LastActivityDate)
                , this.InsertString(this.Title)
                , this.InsertString(this.Tags)
                , this.InsertNumber(this.AnswerCount)
                , this.InsertNumber(this.CommentCount)
                , this.InsertNumber(this.FavoriteCount)
                , this.InsertDate(this.ClosedDate)
                , this.InsertDate(this.CommunityOwnedDate)
            );
        }


    }


    [XmlRoot(ElementName = "posts")]
    public class Posts
    {
        [XmlElement(ElementName = "row")]
        public List<Post> Row { get; set; }
    }


}
