using SimulacaoTrab1.Simulation.Model;
using SimulacaoTrab1.Simulation.Restaurant.entities;

namespace SimulacaoTrab1.Simulation.Restaurant.events
{
    public class SaidaMesaQuatroLugares : Event
    {
        public EntitySet QueueTableForFour { get; set; }
        public Resource TableForFour { get; set; }
        public Entity ClientGroup { get; set; }

        public SaidaMesaQuatroLugares(string name, Entity clientGroup, Resource resource, Scheduler scheduler)
        : base(name, resource, scheduler)
        {
            this.QueueTableForFour = Scheduler.GetEntitySetByName("filaMesa4Lugares");
            this.TableForFour = Scheduler.GetResourceByName("mesa4Lugares");
            this.ClientGroup = clientGroup;
        }

        public override void Execute()
        {
            base.Execute();
            TableForFour.Release(1);
            Scheduler.DestroyEntity(ClientGroup.Id);
            if (QueueTableForFour.Entities.Count > 0)
            {
                Scheduler.ScheduleNow(Scheduler.CreateEvent(new ChegadaMesaQuatroLugares("4 Spots Arrival", (ClientGroup)QueueTableForFour.Remove(), Scheduler)));
            }
        }
    }
}
