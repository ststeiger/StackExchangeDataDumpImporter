/* 
    Licensed under the Apache License, Version 2.0
    
    http://www.apache.org/licenses/LICENSE-2.0
    */
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
namespace Xml2CSharp
{
    [XmlRoot(ElementName="row")]
    public class User {
        [XmlAttribute(AttributeName="Id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName="Reputation")]
        public string Reputation { get; set; }
        [XmlAttribute(AttributeName="CreationDate")]
        public string CreationDate { get; set; }
        [XmlAttribute(AttributeName="DisplayName")]
        public string DisplayName { get; set; }
        [XmlAttribute(AttributeName="LastAccessDate")]
        public string LastAccessDate { get; set; }
        [XmlAttribute(AttributeName="Location")]
        public string Location { get; set; }
        [XmlAttribute(AttributeName="AboutMe")]
        public string AboutMe { get; set; }
        [XmlAttribute(AttributeName="Views")]
        public string Views { get; set; }
        [XmlAttribute(AttributeName="UpVotes")]
        public string UpVotes { get; set; }
        [XmlAttribute(AttributeName="DownVotes")]
        public string DownVotes { get; set; }
        [XmlAttribute(AttributeName="Age")]
        public string Age { get; set; }
        [XmlAttribute(AttributeName="AccountId")]
        public string AccountId { get; set; }
        [XmlAttribute(AttributeName="WebsiteUrl")]
        public string WebsiteUrl { get; set; }
        [XmlAttribute(AttributeName="ProfileImageUrl")]
        public string ProfileImageUrl { get; set; }
    }

    [XmlRoot(ElementName="users")]
    public class Users {
        [XmlElement(ElementName="row")]
        public List<User> Row { get; set; }
    }

}
