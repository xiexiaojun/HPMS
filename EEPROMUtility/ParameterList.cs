using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace EEPROMUtility
{

    [XmlRoot(ElementName = "FirmwareType")]
    public class FirmwareType
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "description")]
        public string Description { get; set; }
    }

    [XmlRoot(ElementName = "WriteContext")]
    public class WriteContext
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "value")]
        public string Value { get; set; }
        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }
        [XmlAttribute(AttributeName = "repeat")]
        public string Repeat { get; set; }
    }

    [XmlRoot(ElementName = "Data")]
    public class Data
    {
        [XmlElement(ElementName = "WriteContext")]
        public List<WriteContext> WriteContext { get; set; }
    }

    [XmlRoot(ElementName = "Command")]
    public class Command
    {
        [XmlAttribute(AttributeName = "mode")]
        public string Mode { get; set; }
        [XmlAttribute(AttributeName = "chip")]
        public string Chip { get; set; }
        [XmlAttribute(AttributeName = "chipOffset")]
        public string ChipOffset { get; set; }
        [XmlAttribute(AttributeName = "context")]
        public string Context { get; set; }
        [XmlAttribute(AttributeName = "start")]
        public string Start { get; set; }
        [XmlAttribute(AttributeName = "length")]
        public string Length { get; set; }
        [XmlAttribute(AttributeName = "remark")]
        public string Remark { get; set; }

    }

    [XmlRoot(ElementName = "Write")]
    public class Write
    {
        [XmlElement(ElementName = "Command")]
        public List<Command> Command { get; set; }
    }

    [XmlRoot(ElementName = "Read")]
    public class Read
    {
        [XmlElement(ElementName = "Command")]
        public List<Command> Command { get; set; }
    }

    [XmlRoot(ElementName = "Action")]
    public class Action
    {
        [XmlElement(ElementName = "Write")]
        public Write Write { get; set; }
        [XmlElement(ElementName = "Read")]
        public Read Read { get; set; }
    }

    [XmlRoot(ElementName = "Ignore")]
    public class Ignore
    {
        [XmlAttribute(AttributeName = "bits")]
        public string Bits { get; set; }
        [XmlAttribute(AttributeName = "enable")]
        public string Enable { get; set; }
    }

    [XmlRoot(ElementName = "Check")]
    public class Check
    {
        [XmlElement(ElementName = "Ignore")]
        public Ignore Ignore { get; set; }
    }

    [XmlRoot(ElementName = "Checksum")]
    public class Checksum
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "start")]
        public string Start { get; set; }
        [XmlAttribute(AttributeName = "end")]
        public string End { get; set; }
        [XmlAttribute(AttributeName = "set")]
        public string Set { get; set; }
    }

    [XmlRoot(ElementName = "ParityBit")]
    public class ParityBit
    {
        [XmlElement(ElementName = "Checksum")]
        public List<Checksum> Checksum { get; set; }
    }

    [XmlRoot(ElementName = "Field")]
    public class Field
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "start")]
        public string Start { get; set; }
        [XmlAttribute(AttributeName = "end")]
        public string End { get; set; }
        [XmlAttribute(AttributeName = "fill")]
        public string Fill { get; set; }
        [XmlAttribute(AttributeName = "convert")]
        public string Convert { get; set; }
    }

    [XmlRoot(ElementName = "KeyField")]
    public class KeyField
    {
        [XmlElement(ElementName = "Field")]
        public List<Field> Field { get; set; }
    }

    [XmlRoot(ElementName = "Display")]
    public class Display
    {
        [XmlElement(ElementName = "ParityBit")]
        public ParityBit ParityBit { get; set; }
        [XmlElement(ElementName = "KeyField")]
        public KeyField KeyField { get; set; }
    }

    [XmlRoot(ElementName = "ParameterList")]
    public class ParameterList
    {
        [XmlElement(ElementName = "FirmwareType")]
        public FirmwareType FirmwareType { get; set; }
        [XmlElement(ElementName = "Data")]
        public Data Data { get; set; }
        [XmlElement(ElementName = "Action")]
        public Action Action { get; set; }
        [XmlElement(ElementName = "Check")]
        public Check Check { get; set; }
        [XmlElement(ElementName = "Display")]
        public Display Display { get; set; }
    }

   
}
