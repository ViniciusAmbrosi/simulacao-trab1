using SimulacaoTrab1;
using SimulacaoTrab1.Reader;
using SimulacaoTrab1.Reader.Model;
using SimulacaoTrab1.Translator;

namespace SimulacaoTrab1.Simulation.Model

{
    public class Waiter
    {
        public PetriNetwork PetriNetwork { get; set; }

        public readonly int replaceCashierPlaceId = 5;

        public readonly int orderReadyPlaceId = 6;

        public readonly int clientWillSitPlaceId = 7;

        public Waiter()
        {    
            PNMLDocument? document = PNMLReader.ReadFile("C:\\Project\\simulacao-trab1\\SimulacaoTrab1\\resources\\waiter.pflow");

            if (document == null)
            {
                Console.WriteLine("XML file malformed, can't process PNML structure.");
            }
            else
            {
                PetriNetwork = PnmlModelToPetriModelTranslator.GetPetryNetwork(document);
            }
        }

        public void AddTokenAt(int id)
        {
            PetriNetwork.AddTokenToPlaceWithID(id, 1);
        }
    }
}
