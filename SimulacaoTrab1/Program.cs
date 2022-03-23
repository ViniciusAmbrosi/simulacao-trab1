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
    PetriNetwork petriNetwork = PnmlModelToPetriModelTranslator.GetPetryNetwork(document);

    int currentNumberOfExecs = 0;
    bool hasAnyEnabledTransition = true;

    Console.WriteLine("\nStarting to execute cycles for petry network -----------------------------------------");
    while (hasAnyEnabledTransition)
    {
        hasAnyEnabledTransition = EvaluateTransitions(petriNetwork);

        //hasAnyEnabledTransition = false;
        //foreach (var transition in petriNetwork.Transitions)
        //{
        //    transition.AttemptToEnableTransition();

        //    if (transition.IsEnabled)
        //    {
        //        hasAnyEnabledTransition = true;
        //    }
        //}

        petriNetwork.DisplayPetriNetwork(currentNumberOfExecs++);

        if (!hasAnyEnabledTransition)
        {
            return;
        }

        foreach (var transition in petriNetwork.Transitions)
        {
            var result = transition.MoveAsync().Result;
        }
    }
}

bool EvaluateTransitions(PetriNetwork petriNetwork)
{
    bool hasAnyEnabledTransition = false;

    foreach (var transition in petriNetwork.Transitions)
    {
        if (transition.InboundConnectors == null ||
                transition.InboundConnectors.Any(connector => connector is InhibitorArc && ((InhibitorArc)connector).IsBlocked))
        {
            //skips in case of blocked or empty
            //handle starvation
            transition.IsEnabled = false;
        }

        List<WeightedArc> weightedInboundArcs = transition.GetWeightedConnectors(transition.InboundConnectors);

        foreach (var arc in weightedInboundArcs)
        {
            Place place = arc.Place;

            //pegando todos os arcos que saem do place em questão
            IEnumerable<Arc> outboundTransitions = weightedInboundArcs.Where(arc => arc.Place.Id == place.Id);


            //primeira iteração T1 reserva
            //segunda iteração T2 ve reserva e ganha prioridade - 1
            //terceira iteração T3 ve reserva e ganha prioridade - 2
        }

        transition.IsEnabled = weightedInboundArcs.All(connector => connector.Place.MarkCounter >= connector.Weight);
        hasAnyEnabledTransition = hasAnyEnabledTransition || transition.IsEnabled;
    }

    return hasAnyEnabledTransition;

    //pega o place 
    //ve se encontra mais de dois outbound do place para transitions
    //ve se tem arc com prioridade para place
    //ve se tem tokens pra suprir todos os arcos (4 tokens - 2 saidas - 2 tokens cada 0 PODE)
    //se não, escolhe maximo que consegue pagar
    //reserva tokens 
    //assigna prioridade para outras transitions
}