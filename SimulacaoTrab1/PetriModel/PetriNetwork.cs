using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulacaoTrab1
{
    public class PetriNetwork
    {
        public List<Arc> Arcs { get; set; }
        public List<Place> Positions { get; set; }
        public List<Transition> Transitions { get; set; }

        public PetriNetwork()
        {
            Arcs = new List<Arc>();
            Positions = new List<Place>();
            Transitions = new List<Transition>();
        }

        public void DisplayPetriNetwork(int currentNumberOfExecs)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append($"Cycle {currentNumberOfExecs} | ");

            foreach (var position in Positions)
            {
                stringBuilder.Append($"{position.Label} - {position.MarkCounter} | ");
            }

            foreach (var transition in Transitions)
            {
                stringBuilder.Append($"{transition.Label} - {transition.IsEnabled} | ");
            }

            Console.WriteLine(stringBuilder.ToString());
        }

        public Place createPosition(int id, string label, int markAmount = 0)
        {
            Place position = new Place(id, label, markAmount);
            Positions.Add(position);

            return position;
        }

        public Transition createTransition(int id, string Label)
        {
            Transition transition = new Transition(id, Label);
            Transitions.Add(transition);

            return transition;
        }

        public Arc createInboundConnection(int id, Place position, Transition transition, bool isWeightedArc, int weight = 1)
        {
            Arc connector = isWeightedArc ?
                new WeightedArc(id, position, transition, weight) :
                new BlockingArc(id, false, position, transition, weight);

            Arcs.Add(connector);
            transition.InboundConnectors.Add(connector);

            return connector;
        }

        public Arc createOutboundConnection(int id, Place position, Transition transition, bool isWeightedArc, int weight = 1)
        {
            Arc connector = isWeightedArc ?
                new WeightedArc(id, position, transition, weight) :
                new BlockingArc(id, false, position, transition, weight);

            Arcs.Add(connector);
            transition.OutboundConnectors.Add(connector);

            return connector;
        }
    }
}
