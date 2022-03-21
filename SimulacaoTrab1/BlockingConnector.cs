using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulacaoTrab1
{
    public class BlockingConnector : Connector
    {
        public BlockingConnector(int ID, bool isBlocked, Position position, Transition transition) 
            : base(ID, position, transition)
        {
            IsBlocked = isBlocked;
        }

        public bool IsBlocked { get; set; }
    }
}
