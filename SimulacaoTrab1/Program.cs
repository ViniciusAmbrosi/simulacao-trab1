using SimulacaoTrab1;
using SimulacaoTrab1.Reader;
using SimulacaoTrab1.Reader.Model;
using SimulacaoTrab1.Translator;

PNMLDocument? document = PNMLReader.ReadFile("C:\\Project\\simulacao-trab1\\SimulacaoTrab1\\resources\\waiter.pflow");

if (document == null)
{
    Console.WriteLine("XML file malformed, can't process PNML structure.");
}
else 
{
    PetriNetwork petriNetwork = PnmlModelToPetriModelTranslator.GetPetryNetwork(document);

    int currentNumberOfExecs = 0;

    Console.WriteLine("\nStarting to execute cycles for petry network -----------------------------------------");
    petriNetwork.DisplayPetriNetwork(currentNumberOfExecs);

    petriNetwork.AddTokenToPlaceWithID(1, 1);
    petriNetwork.DisplayPetriNetwork(currentNumberOfExecs);
    petriNetwork.AttemptToMoveTransitions();

    petriNetwork.DisplayPetriNetwork(++currentNumberOfExecs);
    petriNetwork.AddTokenToPlaceWithID(6, 1);
    petriNetwork.DisplayPetriNetwork(currentNumberOfExecs);
    petriNetwork.AttemptToMoveTransitions();
    petriNetwork.DisplayPetriNetwork(++currentNumberOfExecs);

    petriNetwork.AddTokenToPlaceWithID(15, 1);
    petriNetwork.DisplayPetriNetwork(currentNumberOfExecs);
    petriNetwork.AttemptToMoveTransitions();
    petriNetwork.DisplayPetriNetwork(++currentNumberOfExecs);
}

