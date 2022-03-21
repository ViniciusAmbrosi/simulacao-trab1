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
        public int ID { get; set; }

        public Connector(int ID, Position position, Transition transition)
        {
            this.Position = position;
            this.Transition = transition;
            this.ID = ID;
        }
    }
}
