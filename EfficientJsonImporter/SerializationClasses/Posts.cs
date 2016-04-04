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
    public class Post {
        [XmlAttribute(AttributeName="Id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName="PostTypeId")]
        public string PostTypeId { get; set; }
        [XmlAttribute(AttributeName="AcceptedAnswerId")]
        public string AcceptedAnswerId { get; set; }
        [XmlAttribute(AttributeName="CreationDate")]
        public string CreationDate { get; set; }
        [XmlAttribute(AttributeName="Score")]
        public string Score { get; set; }
        [XmlAttribute(AttributeName="ViewCount")]
        public string ViewCount { get; set; }
        [XmlAttribute(AttributeName="Body")]
        public string Body { get; set; }
        [XmlAttribute(AttributeName="OwnerUserId")]
        public string OwnerUserId { get; set; }
        [XmlAttribute(AttributeName="LastEditorUserId")]
        public string LastEditorUserId { get; set; }
        [XmlAttribute(AttributeName="LastEditDate")]
        public string LastEditDate { get; set; }
        [XmlAttribute(AttributeName="LastActivityDate")]
        public string LastActivityDate { get; set; }
        [XmlAttribute(AttributeName="Title")]
        public string Title { get; set; }
        [XmlAttribute(AttributeName="Tags")]
        public string Tags { get; set; }
        [XmlAttribute(AttributeName="AnswerCount")]
        public string AnswerCount { get; set; }
        [XmlAttribute(AttributeName="CommentCount")]
        public string CommentCount { get; set; }
        [XmlAttribute(AttributeName="FavoriteCount")]
        public string FavoriteCount { get; set; }
    }

    [XmlRoot(ElementName="posts")]
    public class Posts {
        [XmlElement(ElementName="row")]
        public List<Post> Row { get; set; }
    }

}
