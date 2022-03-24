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

            if (place.OutboundConnections.Count > 1)
            {
                Arc arcWithHighestPriority = null;
                foreach(var item in place.OutboundConnections)
                {
                    List<WeightedArc> inboundArcs = item.Transition.GetWeightedConnectors(item.Transition.InboundConnectors);
                    bool hasEnoughMarks = inboundArcs.All(connector => connector.Place.MarkCounter - connector.Place.ReservedMarks >= connector.Weight);
                    if (hasEnoughMarks)
                    {
                        arcWithHighestPriority = item;
                        arcWithHighestPriority.Place.ReservedMarks += arcWithHighestPriority.Weight;
                        break;
                    }
                }

                if (arcWithHighestPriority != null)
                {
                    for (int i = 0; i < place.OutboundConnections.Count; i++)
                    {
                        if (arcWithHighestPriority.Transition.Priority > place.OutboundConnections[i].Transition.Priority)
                        {
                            arcWithHighestPriority = place.OutboundConnections[i];
                        }
                    }

                    arcWithHighestPriority.Transition.ReservedMarks = arcWithHighestPriority.Weight;
                    arcWithHighestPriority.Transition.Priority *= 2;
                }
            } 
        }

        if (transition.ReservedMarks > 0)
        {
            transition.IsEnabled = true;
        } else
        {
            transition.IsEnabled = weightedInboundArcs.All(connector => connector.Place.MarkCounter - connector.Place.ReservedMarks >= connector.Weight);
        }
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