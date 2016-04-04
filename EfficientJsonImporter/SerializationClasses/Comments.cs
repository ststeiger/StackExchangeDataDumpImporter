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
    public class Comment : EfficientJsonImporter.TabularData {
        [XmlAttribute(AttributeName="Id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName="PostId")]
        public string PostId { get; set; }
        [XmlAttribute(AttributeName="Score")]
        public string Score { get; set; }
        [XmlAttribute(AttributeName="Text")]
        public string Text { get; set; }
        [XmlAttribute(AttributeName="CreationDate")]
        public string CreationDate { get; set; }
        [XmlAttribute(AttributeName="UserId")]
        public string UserId { get; set; }
    }

    [XmlRoot(ElementName="comments")]
    public class Comments {
        [XmlElement(ElementName="row")]
        public List<Comment> Row { get; set; }
    }


    public override string FileName
    { 
        get{ 
            return "Comments.xml";
        }
    }


    public override void InsertRow(System.Text.StringBuilder sb)
    {
        string strSQL =@"
    INSERT INTO badges(id, userid, name, date) 
    VALUES ({0}, {1}, {2}, {3}); 
    ";

        sb.AppendFormat(strSQL);
    }
}
