// See https://aka.ms/new-console-template for more information
using SimulacaoTrab1;
using SimulacaoTrab1.Reader;
using SimulacaoTrab1.Reader.Model;
using SimulacaoTrab1.Translator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

PNMLDocument? document = PNMLReader.ReadFile();

if (document == null)
{
    Console.WriteLine("XML file malformed, can't process PNML structure.");
}
else 
{
    PetriNetwork petriNetwork = PnmlModelToPetriModelTranslator.GetTransitions(document);

    int currentNumberOfExecs = 0;
    bool hasAnyEnabledTransition = true;

    while (hasAnyEnabledTransition)
    {
        hasAnyEnabledTransition = false;
        foreach (var transition in petriNetwork.Transitions)
        {
            transition.AttemptToEnableTransition();

            if (transition.IsEnabled)
            {
                hasAnyEnabledTransition = true;
            }
        }

        petriNetwork.DisplayPetriNetwork(currentNumberOfExecs);

        if (!hasAnyEnabledTransition)
        {
            return;
        }

        foreach (var transition in petriNetwork.Transitions)
        {
            var result = transition.MoveAsync().Result;
        }

        currentNumberOfExecs++;
    }
}