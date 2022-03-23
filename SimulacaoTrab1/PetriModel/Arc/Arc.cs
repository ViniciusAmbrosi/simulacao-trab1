using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulacaoTrab1
{
    public class Arc
    {
        public Place Place { get; set; }
        public Transition Transition { get; set; }
        public int Id { get; set; }
        public int Weight { get; set; }

        public Arc(int id, Place position, Transition transition, int weight)
        {
            this.Place = position;
            this.Transition = transition;
            this.Id = id;
            this.Weight = weight;
        }
    }
}
