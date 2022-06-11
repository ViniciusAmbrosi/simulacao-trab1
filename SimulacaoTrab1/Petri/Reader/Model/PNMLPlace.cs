using System.Xml.Serialization;

namespace SimulacaoTrab1.Reader.Model
{
    [Serializable]
    public class PNMLPlace
    {
        [XmlElement(ElementName = "id")]
        public int Id { get; set; }

        [XmlElement(ElementName = "label")]
        public string Label { get; set; }

        [XmlElement(ElementName = "tokens")]
        public int Tokens { get; set; }
    }
}
