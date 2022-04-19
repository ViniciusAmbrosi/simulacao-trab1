using SimulacaoTrab1.Reader.Model;
using System.Xml.Serialization;

namespace SimulacaoTrab1.Reader
{
    public static class PNMLReader
    {
        public static PNMLDocument? ReadFile(string filePath = "C:\\Project\\simulacao-trab1\\SimulacaoTrab1\\resources\\newmodel.pflow")
        {
            string myFile = File.ReadAllText(filePath);

            XmlSerializer serializer = new XmlSerializer(typeof(PNMLDocument));

            using (StringReader reader = new StringReader(myFile))
            {
                var result = serializer.Deserialize(reader);

                if (result != null) {
                    return (PNMLDocument) result;
                }
            }

            return null;
        }
    }
}
