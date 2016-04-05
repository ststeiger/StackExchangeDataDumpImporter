
using System.Xml.Serialization;
using System.Collections.Generic;


namespace Xml2CSharp
{


    [XmlRoot(ElementName = "row")]
    public class User : EfficientJsonImporter.TabularData
    {

        [XmlAttribute(AttributeName = "Id")]
        public long Id { get; set; }

        [XmlAttribute(AttributeName = "Reputation")]
        public long Reputation { get; set; }

        [XmlAttribute(AttributeName = "CreationDate")]
        public System.DateTime CreationDate { get; set; }

        [XmlAttribute(AttributeName = "DisplayName")]
        public string DisplayName { get; set; }

        [XmlAttribute(AttributeName = "LastAccessDate")]
        public System.DateTime LastAccessDate { get; set; }

        [XmlAttribute(AttributeName = "WebsiteUrl")]
        public string WebsiteUrl { get; set; }

        [XmlAttribute(AttributeName = "Location")]
        public string Location { get; set; }

        [XmlAttribute(AttributeName = "AboutMe")]
        public string AboutMe { get; set; }

        [XmlAttribute(AttributeName = "Views")]
        public long Views { get; set; }

        [XmlAttribute(AttributeName = "UpVotes")]
        public long UpVotes { get; set; }

        [XmlAttribute(AttributeName = "DownVotes")]
        public long DownVotes { get; set; }

        [XmlAttribute(AttributeName = "ProfileImageUrl")]
        public string ProfileImageUrl { get; set; }

        [XmlAttribute(AttributeName = "EmailHash")]
        public string EmailHash { get; set; }

        [XmlAttribute(AttributeName = "Age")]
        public int? Age { get; set; }

        [XmlAttribute(AttributeName = "AccountId")]
        public long? AccountId { get; set; }


        public override string FileName
        {
            get
            {
                return "Users.xml";
            }
        }


        public override void InsertRow(System.Text.StringBuilder sb)
        {
            string strSQL = @"
INSERT INTO Users
( 
     Id, Reputation, CreationDate, DisplayName, LastAccessDate
    ,WebsiteUrl, Location, AboutMe, Views, UpVotes, DownVotes
    ,ProfileImageUrl, EmailHash, Age, AccountId
)
VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}); 
";
            
            sb.AppendFormat(strSQL
                , this.Id
                , this.InsertNumber(this.Reputation)
                , this.InsertDate(this.CreationDate)
                , this.InsertString(this.DisplayName)
                , this.InsertDate(this.LastAccessDate)
                , this.InsertString(this.WebsiteUrl)
                , this.InsertString(this.Location)
                , this.InsertString(this.AboutMe)
                , this.InsertNumber(this.Views)
                , this.InsertNumber(this.UpVotes)
                , this.InsertNumber(this.DownVotes)
                , this.InsertString(this.ProfileImageUrl)
                , this.InsertString(this.EmailHash)
                , this.InsertNumber(this.Age)
                , this.InsertNumber(this.AccountId)
            );
        }
    }


    [XmlRoot(ElementName = "users")]
    public class Users
    {
        [XmlElement(ElementName = "row")]
        public List<User> Row { get; set; }
    }


}
