using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulacaoTrab1
{
    public class Position
    {
        public Position(int ID, int markCounter = 1)
        {
            this.MarkCounter = markCounter;
            this.ID = ID;
        }

        public int MarkCounter { get; set; }
        public int ID { get; set; }
    }
}
