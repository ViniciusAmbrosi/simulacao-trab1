using System.Xml.Serialization;

namespace SimulacaoTrab1.Reader.Model
{
    [Serializable, XmlRoot("document")]
    public class PNMLDocument
    {
        [XmlElement(ElementName = "subnet")]
        public PNMLSubnet subnet { get; set; }
    }
}
