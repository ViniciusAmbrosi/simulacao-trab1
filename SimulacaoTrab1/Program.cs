//using SimulacaoTrab1;
//using SimulacaoTrab1.Reader;
//using SimulacaoTrab1.Reader.Model;
//using SimulacaoTrab1.Translator;

//PNMLDocument? document = PNMLReader.ReadFile("C:\\Project\\simulacao-trab1\\SimulacaoTrab1\\resources\\newmodel.pflow");

//if (document == null)
//{
//    Console.WriteLine("XML file malformed, can't process PNML structure.");
//}
//else 
//{
//    PetriNetwork petriNetwork = PnmlModelToPetriModelTranslator.GetPetryNetwork(document);

//    int currentNumberOfExecs = 0;
//    bool hasAnyEnabledTransition = true;

//    Console.WriteLine("\nStarting to execute cycles for petry network -----------------------------------------");
//    while (hasAnyEnabledTransition)
//    {
//        foreach (var transition in petriNetwork.Transitions)
//        { 
//            transition.AttemptToEnableTransition();
//        }

//        hasAnyEnabledTransition = petriNetwork.Transitions.Any(transition => transition.Enabled);

//        petriNetwork.DisplayPetriNetwork(currentNumberOfExecs++);

//        if (!hasAnyEnabledTransition)
//        {
//            return;
//        }

//        foreach (var transition in petriNetwork.Transitions)
//        {
//            var result = transition.MoveAsync().Result;
//        }

//        //execute delayed actions and cleanup for further execution
//        petriNetwork.Places.ForEach(place => {
//            place.DelayedActions.ForEach(action => action());
//            place.DelayedActions = new List<Action>();
//        });
//    }
//}

