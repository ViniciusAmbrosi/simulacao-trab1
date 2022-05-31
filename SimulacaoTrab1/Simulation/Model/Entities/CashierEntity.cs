
namespace SimulacaoTrab1.Simulation.Model.Entities
{
    public class CashierEntity : Entity
    {
        public CashierEntity(
            string name, 
            PetriNetwork? petriNetwork = null,
            Resource? resource = null,
            int priority = -1) 
            : base(name, petriNetwork, resource, priority)
        {
        }
    }
}
