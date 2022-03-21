using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulacaoTrab1
{
    public class ResetConnector : WeightedConnector
    {
        public ResetConnector(int ID, Position position, Transition transition, int weight) : 
            base(ID, position, transition, weight)
        {
        }
    }
}
