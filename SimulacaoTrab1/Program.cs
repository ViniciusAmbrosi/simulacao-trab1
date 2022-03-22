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

    while (currentNumberOfExecs <= 10)
    {

        bool allInactive = true;
        foreach (var transition in petriNetwork.transitions)
        {
            transition.AttemptToEnableTransition();

            if (transition.IsEnabled)
            {
                allInactive = false;
            }
        }

        petriNetwork.PrintPetriNetwork(currentNumberOfExecs);

        if (allInactive)
        {
            return;
        }

        foreach (var transition in petriNetwork.transitions)
        {
            var result = transition.MoveAsync().Result;
        }

        Console.WriteLine();
        currentNumberOfExecs++;
    }
}



//PetriNetwork petriNetwork = new PetriNetwork();
//int positionID = 1;
//int transitionID = 1;
//int connectionID = 1;

//Position L1 = petriNetwork.createPosition(positionID++, 2);
//Transition T1 = petriNetwork.createTransition(transitionID++);
//Arc L1ToT1 = petriNetwork.createInboundConnection(connectionID++, L1, T1);

//Position L2 = petriNetwork.createPosition(positionID++);
//Transition T2 = petriNetwork.createTransition(transitionID++);
//Arc T1ToL2 = petriNetwork.createOutboundConnection(connectionID++, L2, T1);
//Arc L2ToT2 = petriNetwork.createInboundConnection(connectionID++, L2, T2);

//Position L3 = petriNetwork.createPosition(positionID++, 2);
//Arc L3ToT2 = petriNetwork.createInboundConnection(connectionID++, L3, T2, 2);

//Position L4 = petriNetwork.createPosition(positionID++);
//Transition T3 = petriNetwork.createTransition(transitionID++);
//Arc T2ToL4 = petriNetwork.createOutboundConnection(connectionID++, L4, T2);
//Arc L4ToT3 = petriNetwork.createInboundConnection(connectionID++, L4, T3);

//Position L5 = petriNetwork.createPosition(positionID++, 5);
//Arc L5ToT2 = petriNetwork.createInboundConnection(connectionID++, L5, T2, 3);

//Transition T4 = petriNetwork.createTransition(transitionID++);
//Position L6 = petriNetwork.createPosition(positionID++);
//Position L7 = petriNetwork.createPosition(positionID++);

//Arc T3ToL3 = petriNetwork.createOutboundConnection(connectionID++, L3, T3, 2);
//Arc T3ToL6 = petriNetwork.createOutboundConnection(connectionID++, L6, T3);
//Arc T3ToL7 = petriNetwork.createOutboundConnection(connectionID++, L7, T3);

//Arc L6ToT4 = petriNetwork.createInboundConnection(connectionID++, L6, T4);
//Arc L7ToT4 = petriNetwork.createInboundConnection(connectionID++, L7, T4);
//Arc T4ToL5 = petriNetwork.createOutboundConnection(connectionID++, L5, T4, 3);

//Position L8 = petriNetwork.createPosition(positionID++);
//Arc T4ToL8 = petriNetwork.createOutboundConnection(connectionID++, L8, T4);

//int currentNumberOfExecs = 1;

//while (currentNumberOfExecs <= 10)
//{

//    bool allInactive = true;
//    foreach (var transition in petriNetwork.idToTransition.Values)
//    {
//        transition.AttemptToEnableTransition();

//        if (transition.IsEnabled)
//        {
//            allInactive = false;
//        }
//    }

//    Console.WriteLine("Cicle {0}, L1 - {1}, L2 - {2}, L3 - {3}, L4 - {4}, L5 - {5}, L6 - {6}, L7 - {7}, L8 - {8}, T1 - {9}, T2 - {10}, T3 - {11}, T4 - {12}",
//        currentNumberOfExecs, L1.MarkCounter, L2.MarkCounter, L3.MarkCounter, L4.MarkCounter, L5.MarkCounter, L6.MarkCounter, L7.MarkCounter, L8.MarkCounter,
//        T1.IsEnabled, T2.IsEnabled, T3.IsEnabled, T4.IsEnabled);

//    if (allInactive)
//    {
//        return;
//    }

//    foreach (var transition in petriNetwork.idToTransition.Values)
//    {
//        var result = transition.MoveAsync().Result;
//    }

//    Console.WriteLine();
//    currentNumberOfExecs++;
//}