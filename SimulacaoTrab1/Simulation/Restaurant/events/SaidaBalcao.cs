using SimulacaoTrab1.Simulation.Model;
using SimulacaoTrab1.Simulation.Restaurant.entities;

namespace SimulacaoTrab1.Simulation.Restaurant.events
{
    public class SaidaBalcao : Event
    {
        public EntitySet QueueDesk { get; set; }
        public Resource Desk { get; set; }
        public Entity ClientGroup { get; set; }

        public SaidaBalcao(string name, Entity clientGroup, Resource resource, Scheduler scheduler)
        : base(name, resource, scheduler)
        {
            this.QueueDesk = Scheduler.GetEntitySetByName("filaBalcao");
            this.Desk = Scheduler.GetResourceByName("balcao");
            this.ClientGroup = clientGroup;
        }

        public override void Execute()
        {
            base.Execute();
            Desk.Release(1);
            Scheduler.DestroyEntity(ClientGroup.Id);
            if (QueueDesk.Entities.Count > 0)
            {
                Scheduler.ScheduleNow(Scheduler.CreateEvent(new ChegadaBalcao("1 Spot Arrival", (ClientGroup)QueueDesk.Remove(), Scheduler)));
            }
        }
    }
}
