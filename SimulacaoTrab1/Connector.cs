using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulacaoTrab1
{
    public class Connector
    {
        public Position Position { get; set; }
        public Transition Transition { get; set; }

        public Connector(Position position, Transition transition)
        {
            this.Position = position;
            this.Transition = transition;
        }
    }
}
