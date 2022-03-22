﻿using System.Xml.Serialization;

namespace SimulacaoTrab1.Reader.Model
{
    [Serializable]
    public class PNMLTransition {
        [XmlElement(ElementName = "id")]
        public int id { get; set; }

        [XmlElement(ElementName = "label")]
        public string Label { get; set; }
    }
}
