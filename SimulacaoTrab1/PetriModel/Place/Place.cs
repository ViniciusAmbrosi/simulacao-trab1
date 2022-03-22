using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulacaoTrab1
{
    public class Place
    {
        public Place(int id, string label, int markCounter = 1)
        {
            this.MarkCounter = markCounter;
            this.Id = id;
            this.Label = label;
        }

        public int MarkCounter { get; set; }
        public int Id { get; set; }
        public string Label { get; set; }   
    }
}
