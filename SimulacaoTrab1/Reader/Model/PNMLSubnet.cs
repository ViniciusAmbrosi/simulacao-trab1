using System.Xml.Serialization;

namespace SimulacaoTrab1.Reader.Model
{
    [Serializable]
    public class PNMLSubnet
    {
        [XmlElement(ElementName = "place")]
        public PNMLPlace[] Places { get; set; }

        [XmlElement(ElementName = "transition")]
        public PNMLTransition[] Transitions { get; set; }

        [XmlElement(ElementName = "arc")]
        public PNMLArc[] Arcs { get; set; }
    }
}
