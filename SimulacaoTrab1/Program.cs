// See https://aka.ms/new-console-template for more information
using SimulacaoTrab1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

Console.WriteLine("Hello, World!");

List<Transition> transitions = new List<Transition>();

Position initialPosition = new Position(1);
Position intermediatePosition = new Position(0);
Position finalPosition = new Position(1);

Transition transitionToIntermediate = new Transition(0);
transitions.Add(transitionToIntermediate);

WeightedConnector inboundPosition = new WeightedConnector(position: initialPosition, transition: transitionToIntermediate);
WeightedConnector outboundPosition = new WeightedConnector(position: intermediatePosition, transition: transitionToIntermediate);

transitionToIntermediate.InboundConnectors = new List<Connector> { inboundPosition };
transitionToIntermediate.OutboundConnectors = new List<Connector> { outboundPosition };

Transition transitionToFinal = new Transition(1);
transitions.Add(transitionToFinal);

WeightedConnector finalInboundPosition = new WeightedConnector(position: intermediatePosition, transition: transitionToFinal);
WeightedConnector finalOutboundPosition = new WeightedConnector(position: finalPosition, transition: transitionToFinal);

transitionToFinal.InboundConnectors = new List<Connector>() { finalInboundPosition };
transitionToFinal.OutboundConnectors = new List<Connector>() { finalOutboundPosition };

Console.WriteLine("Marcas no inicial {0}", initialPosition.MarkCounter);
Console.WriteLine("Marcas no inicial {0}", intermediatePosition.MarkCounter);
Console.WriteLine("Marcas no final {0}", finalPosition.MarkCounter);
int i = 0;
while (i++ < 5)
{
    Console.WriteLine("VERIFICA ELIGIBILIDADE -------------------------------------------");
    transitions.ForEach(transition =>
    {
        transition.AttemptToEnableTransition();
        Console.WriteLine("Transition {0} is enabled={1}", transition.TransitionId, transition.IsEnabled);
    });
    Console.WriteLine();

    Console.WriteLine("MOVEIMENTOS -------------------------------------------");
    transitions.ForEach(transition =>
    {
        Console.WriteLine(transition.MoveAsync().Result);
    });
    Console.WriteLine();
}


Console.WriteLine("Marcas no inicial {0}", initialPosition.MarkCounter);
Console.WriteLine("Marcas no inicial {0}", intermediatePosition.MarkCounter);
Console.WriteLine("Marcas no final {0}", finalPosition.MarkCounter);



// 0 -> | -> 0 -> | -> 0