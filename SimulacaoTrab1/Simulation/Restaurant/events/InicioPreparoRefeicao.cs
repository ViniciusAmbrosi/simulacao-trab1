using SimulacaoTrab1.Simulation.Model;
using SimulacaoTrab1.Simulation.Restaurant.entities;

namespace SimulacaoTrab1.Simulation.Restaurant.events
{
    public class InicioPreparoRefeicao : Event
    {
        private ClientGroup ClientGroup { get; set; }

        public InicioPreparoRefeicao(string name, ClientGroup clientGroup, Scheduler scheduler)
        : base(name, scheduler)
        {

            this.ClientGroup = clientGroup;
            this.Resource = Scheduler.GetResourceByName("cozinha");
            this.EntitySet = Scheduler.GetEntitySetByName("filaCozinha");
        }

        public new void Execute()
        {
            base.Execute();
            if (Resource.Allocate(1))
            {
                Scheduler.ScheduleIn(Scheduler.CreateEvent(new TerminoPreparoRefeicao("Termino Preparo Refeicao", ClientGroup, Resource, Scheduler)), Scheduler.Normal(14, 5));
            }
            else
            {
                EntitySet.Insert(ClientGroup);
            }
        }
    }
}
