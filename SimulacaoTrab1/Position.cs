using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulacaoTrab1
{
    public class Position
    {
        public Position(int Id, string label, int markCounter = 1)
        {
            this.MarkCounter = markCounter;
            this.Id = Id;
            this.Label = label;
        }

        public int MarkCounter { get; set; }
        public int Id { get; set; }

        public string Label { get; set; }   
    }
}
