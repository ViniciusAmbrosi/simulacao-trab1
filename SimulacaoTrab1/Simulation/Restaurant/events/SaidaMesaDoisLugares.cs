using SimulacaoTrab1.Simulation.Model;
using SimulacaoTrab1.Simulation.Restaurant.entities;

namespace SimulacaoTrab1.Simulation.Restaurant.events
{
    public class SaidaMesaDoisLugares : Event
    {
        public EntitySet QueueTableForTwo { get; set; }
        public Resource TableForTwo { get; set; }
        public Entity ClientGroup { get; set; }

        public SaidaMesaDoisLugares(string name, Entity clientGroup, Resource resource, Scheduler scheduler)
        : base(name)
        {
            this.Resource = resource;
            this.Scheduler = scheduler;

            this.QueueTableForTwo = Scheduler.GetEntitySetByName("filaMesa2Lugares");
            this.TableForTwo = Scheduler.GetResourceByName("mesa2Lugares");
            this.ClientGroup = clientGroup;
        }

        public override void Execute()
        {
            base.Execute();
            TableForTwo.Release(1);
            Scheduler.DestroyEntity(ClientGroup.Id);
            if (QueueTableForTwo.Entities.Count > 0)
            {
                Scheduler.ScheduleNow(Scheduler.CreateEvent(new ChegadaMesaDoisLugares("2 Spots Arrival", (ClientGroup)QueueTableForTwo.Remove(), Scheduler)));
            }
        }
    }
}
