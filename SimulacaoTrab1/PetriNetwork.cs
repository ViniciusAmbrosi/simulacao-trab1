using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulacaoTrab1
{
    public class PetriNetwork
    {
        public IDictionary<int, Connector> idToconnector { get; set; }
        public IDictionary<int, Position> idToPosition { get; set; }
        public IDictionary<int, Transition> idToTransition { get; set; }

        public PetriNetwork()
        {
            idToconnector = new Dictionary<int, Connector>();
            idToPosition = new Dictionary<int, Position>();
            idToTransition = new Dictionary<int, Transition>();
        }

        public Position createPosition(int ID, int markAmount = 0)
        {
            Position position = new Position(ID, markAmount);
            idToPosition.Add(ID, position);
            return position;
        }

        public bool deletePosition(int ID)
        {
            return idToPosition.Remove(ID);
        }

        public Transition createTransition(int ID)
        {
            Transition transition = new Transition(ID);
            idToTransition.Add(ID, transition);
            return transition;
        }

        public bool deleteTransition(int ID)
        {
            return idToTransition.Remove(ID);
        }

        public Connector createInboundConnection(int ID, Position position, Transition transition, int weight = 1)
        {
            Connector connector = new WeightedConnector(ID, position, transition, weight);
            idToconnector.Add(ID, connector);
            transition.InboundConnectors.Add(connector);

            return connector;
        }

        public Connector createOutboundConnection(int ID, Position position, Transition transition, int weight = 1)
        {
            Connector connector = new WeightedConnector(ID, position, transition, weight);
            idToconnector.Add(ID, connector);
            transition.OutboundConnectors.Add(connector);

            return connector;
        }

        public bool deleteConnector(int ID)
        {
            return idToconnector.Remove(ID);
        }
    }
}
