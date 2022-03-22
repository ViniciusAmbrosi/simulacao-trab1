using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulacaoTrab1
{
    public class ResetArc : WeightedArc
    {
        public ResetArc(int id, Place position, Transition transition, int weight) : 
            base(id, position, transition, weight)
        {
        }
    }
}
