using SimulacaoTrab1.Simulation.Model;
using SimulacaoTrab1.Simulation.Restaurant.entities;

namespace SimulacaoTrab1.Simulation.Restaurant.events
{
    public class InicioRefeicao : Event
    {
        public ClientGroup ClientGroup { get; set; }

        public InicioRefeicao(string name, ClientGroup grupo, Scheduler scheduler)
        : base(name, scheduler)
        {
            this.ClientGroup = grupo;
            this.EntitySet = Scheduler.GetEntitySetByName("filaComidaPronta");

            SetResourceByQuantity(grupo);
        }

        public new void Execute()
        {
            base.Execute();


            Entity groupOrder = EntitySet.RemoveById(ClientGroup.Id);

            /* Se comida do grupo não está pronta retorna e aguarda a finalização da cozinha chamar esse evento  */
            if (groupOrder == null)
            {
                return;
            }

            Scheduler.ScheduleIn(Scheduler.CreateEvent(new TerminoRefeicao("Termino Refeicao", ClientGroup, Resource, Scheduler)), Scheduler.Normal(20, 8));
        }

        private void SetResourceByQuantity(ClientGroup clientGroup)
        {
            if (clientGroup.Quantity == 1)
            {
                this.Resource = Scheduler.GetResourceByName("balcao");
            }
            else if (clientGroup.Quantity == 2)
            {
                this.Resource = Scheduler.GetResourceByName("mesa2Lugares");
            }
            else
            {
                this.Resource = Scheduler.GetResourceByName("mesa4Lugares");
            }
        }
    }
}
