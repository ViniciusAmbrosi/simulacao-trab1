using SimulacaoTrab1.PetriModel.Arc;
using System.Text;

namespace SimulacaoTrab1
{
    public class PetriNetwork
    {
        public List<Arc> Arcs { get; set; } = new List<Arc> { };
        public List<Place> Places { get; set; } = new List<Place> { };
        public List<Transition> Transitions { get; set; } = new List<Transition> { };

        public void DisplayPetriNetwork(int currentNumberOfExecs)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append($"Cycle {currentNumberOfExecs} | ");

            foreach (var position in Places)
            {
                stringBuilder.Append($"{position.Label} - {position.MarkCounter} | ");
            }

            foreach (var transition in Transitions)
            {
                stringBuilder.Append($"{transition.Label} - {transition.IsEnabled} | ");
            }

            Console.WriteLine(stringBuilder.ToString());
        }

        public Place createPosition(int id, string label, int tokens = 0)
        {
            Console.WriteLine("Loading place with id {0} label {1} tokens {2}", id, label, tokens);

            Place position = new Place(id, label, tokens);
            Places.Add(position);

            return position;
        }

        public Transition createTransition(int id, string label)
        {
            Console.WriteLine("Loading transition with id {0} label {1}", id, label);

            Transition transition = new Transition(id, label);
            Transitions.Add(transition);

            return transition;
        }

        public Arc? createInboundConnection(int id, Place position, Transition transition, ArcType arcType, int weight = 1)
        {
            Arc? arc = CreateArc(id, position, transition, arcType, weight, true);

            if (arc == null)
            {
                Console.WriteLine("Arc is not of any known type");
            }
            else
            {
                Arcs.Add(arc);
                transition.InboundConnectors.Add(arc);
            }

            return arc;
        }

        public Arc? createOutboundConnection(int id, Place position, Transition transition, ArcType arcType, int weight = 1)
        {
            Arc? arc = CreateArc(id, position, transition, arcType, weight);

            if (arc == null)
            {
                Console.WriteLine("Arc is not of any known type");
            }
            else 
            {
                Arcs.Add(arc);
                transition.OutboundConnectors.Add(arc);
            }

            return arc;
        }

        private Arc? CreateArc(int id, Place position, Transition transition, ArcType arcType, int weight, bool isInbound = false) 
        {
            Arc? arc = null;

            if (arcType == ArcType.regular)
            {
                arc = new WeightedArc(id, position, transition, weight);
                Console.WriteLine("Loading regular arc for position {0} {2} transition {1}", position.Label, transition.Label, isInbound ? "->" : "<-"); 
            }
            else if (arcType == ArcType.inhibitor)
            {
                arc = new InhibitorArc(id, false, position, transition, weight);
                Console.WriteLine("Loading blocking arc for position {0} {2} transition {1}", position.Label, transition.Label, isInbound ? "->" : "<-");
            }
            else if (arcType == ArcType.reset)
            {
                arc = new ResetArc(id, position, transition, weight);
                Console.WriteLine("Loading reset arc for position {0} {2} transition {1}", position.Label, transition.Label, isInbound ? "->" : "<-");
            }
            else 
            {
                Console.WriteLine("Arc is not of any known type");
            }

            return arc;
        }
    }
}
