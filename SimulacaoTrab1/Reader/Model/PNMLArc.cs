using System.Xml.Serialization;

namespace SimulacaoTrab1.Reader.Model
{
    [Serializable]
    public class PNMLArc
    {
        [XmlElement(ElementName = "type")]
        public PNMLArcType type { get; set; }

        [XmlElement(ElementName = "sourceId")]
        public int sourceId { get; set; }

        [XmlElement(ElementName = "destinationId")]
        public int destinationid { get; set; }

        [XmlElement(ElementName = "multiplicity")]
        public int multiplicity { get; set; } 
    }
}
