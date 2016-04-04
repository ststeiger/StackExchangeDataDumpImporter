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
    public class Vote {
        [XmlAttribute(AttributeName="Id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName="PostId")]
        public string PostId { get; set; }
        [XmlAttribute(AttributeName="VoteTypeId")]
        public string VoteTypeId { get; set; }
        [XmlAttribute(AttributeName="CreationDate")]
        public string CreationDate { get; set; }
    }

    [XmlRoot(ElementName="votes")]
    public class Votes {
        [XmlElement(ElementName="row")]
        public List<Vote> Row { get; set; }
    }

}
