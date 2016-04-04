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
    public class Tag {
        [XmlAttribute(AttributeName="Id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName="TagName")]
        public string TagName { get; set; }
        [XmlAttribute(AttributeName="Count")]
        public string Count { get; set; }
        [XmlAttribute(AttributeName="ExcerptPostId")]
        public string ExcerptPostId { get; set; }
        [XmlAttribute(AttributeName="WikiPostId")]
        public string WikiPostId { get; set; }
    }

    [XmlRoot(ElementName="tags")]
    public class Tags {
        [XmlElement(ElementName="row")]
        public List<Tag> Row { get; set; }
    }

}
