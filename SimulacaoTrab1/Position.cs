using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulacaoTrab1
{
    public class Position
    {
        public Position(int markCounter = 1)
        {
            this.MarkCounter = markCounter;
        }

        public int MarkCounter { get; set; }
    }
}
