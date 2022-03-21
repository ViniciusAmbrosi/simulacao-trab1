using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulacaoTrab1
{
    public class WeightedConnector : Connector
    {
        public WeightedConnector(int ID, Position position, Transition transition, int weight = 1)
            : base(ID, position, transition)
        {
            this.Weight = weight;
        }

        public int Weight { get; set; }
    }
}
