using SimulacaoTrab1.Simulation.Model;
using SimulacaoTrab1.Simulation.Restaurant.entities;

namespace SimulacaoTrab1.Simulation.Restaurant.events
{
    public class ChegadaMesaQuatroLugares : Event
    {
        public ClientGroup ClientGroup { get; set; }
        public EntitySet QueueFoodReady { get; set; }
        public EntitySet WaitTable { get; set; }

        public ChegadaMesaQuatroLugares(string name, ClientGroup clientGroup, Scheduler scheduler)
        : base(name, scheduler)
        {
            this.ClientGroup = clientGroup;
            this.Resource = Scheduler.GetResourceByName("mesa4Lugares");
            this.EntitySet = Scheduler.GetEntitySetByName("filaMesa4Lugares");
            this.QueueFoodReady = Scheduler.GetEntitySetByName("filaComidaPronta");
            this.WaitTable = Scheduler.GetEntitySetByName("esperandoM4");
        }

        public new void Execute()
        {
            base.Execute();

            if (Resource.Allocate(1))
            {
                if (QueueFoodReady.GetById(ClientGroup.Id) != null)
                { // se refeicao ja ta pronta. Se não, TerminoPreparoRefeicao irá agendar
                    Scheduler.ScheduleNow(Scheduler.CreateEvent(new InicioRefeicao("Inicio Refeição", ClientGroup, Scheduler)));
                }
                else
                {
                    WaitTable.Insert(ClientGroup);

                    Entity groupReady = WaitTable.Entities.Find(groupWaiting => WaitTable.GetById(groupWaiting.Id) != null);

                    if (groupReady != null)
                    {
                        Scheduler.ScheduleNow(Scheduler.CreateEvent(new InicioRefeicao("Inicio Refeição", (ClientGroup)groupReady, Scheduler)));
                    }
                }
            }
            else
            {
                EntitySet.Insert(ClientGroup);
            }
        }
    }
}
