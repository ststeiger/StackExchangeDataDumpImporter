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
    public class Row {
        [XmlAttribute(AttributeName="Id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName="PostHistoryTypeId")]
        public string PostHistoryTypeId { get; set; }
        [XmlAttribute(AttributeName="PostId")]
        public string PostId { get; set; }
        [XmlAttribute(AttributeName="RevisionGUID")]
        public string RevisionGUID { get; set; }
        [XmlAttribute(AttributeName="CreationDate")]
        public string CreationDate { get; set; }
        [XmlAttribute(AttributeName="UserId")]
        public string UserId { get; set; }
        [XmlAttribute(AttributeName="Text")]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName="posthistory")]
    public class Posthistory {
        [XmlElement(ElementName="row")]
        public List<Row> Row { get; set; }
    }

}
