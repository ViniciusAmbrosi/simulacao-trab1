using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulacaoTrab1
{
    public class Arc
    {
        public Position Position { get; set; }
        public Transition Transition { get; set; }
        public int Id { get; set; }
        public int Weight { get; set; }

        public Arc(int Id, Position position, Transition transition, int weight)
        {
            this.Position = position;
            this.Transition = transition;
            this.Id = Id;
            this.Weight = weight;
        }
    }
}
