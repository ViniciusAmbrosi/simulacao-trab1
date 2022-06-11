using SimulacaoTrab1.Simulation.Model;

namespace SimulacaoTrab1.Simulation.Restaurant.entities
{
    public class ClientGroup : Entity
    {
        public int Quantity { get; set; }

        public ClientGroup(string name, int quantity, Scheduler scheduler)
        : base(name, scheduler)
        {
            this.Quantity = quantity;
        }
    }
}
