using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulacaoTrab1
{
    public class PetriNetwork
    {
        public List<Arc> arcs { get; set; }
        public List<Position> positions { get; set; }
        public List<Transition> transitions { get; set; }

        public PetriNetwork()
        {
            arcs = new List<Arc>();    
            positions = new List<Position>();
            transitions = new List<Transition>(); 
        }

        public void PrintPetriNetwork(int currentNumberOfExecs) {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append($"Cycle {currentNumberOfExecs} | ");

            foreach (var position in positions)
            {
                stringBuilder.Append($"{position.Label} - {position.MarkCounter} | ");
            }

            foreach (var transition in transitions)
            {
                stringBuilder.Append($"{transition.Label} - {transition.IsEnabled} | ");
            }

            Console.WriteLine(stringBuilder.ToString());
        }

        public Position createPosition(int Id, string label, int markAmount = 0)
        {
            Position position = new Position(Id, label, markAmount);
            positions.Add(position);
            return position;
        }

        public Transition createTransition(int Id, string Label)
        {
            Transition transition = new Transition(Id, Label);
            transitions.Add(transition);
            return transition;
        }

        public Arc createInboundConnection(int Id, Position position, Transition transition, bool isWeightedArc, int weight = 1)
        {
            Arc connector = isWeightedArc ? 
                new WeightedArc(Id, position, transition, weight) : 
                new BlockingArc(Id, false, position, transition, weight);

            arcs.Add(connector);
            transition.InboundConnectors.Add(connector);

            return connector;
        }

        public Arc createOutboundConnection(int Id, Position position, Transition transition, bool isWeightedArc, int weight = 1)
        {
            Arc connector = isWeightedArc ? 
                new WeightedArc(Id, position, transition, weight) : 
                new BlockingArc(Id, false, position, transition, weight);

            arcs.Add(connector);
            transition.OutboundConnectors.Add(connector);

            return connector;
        }
    }
}
