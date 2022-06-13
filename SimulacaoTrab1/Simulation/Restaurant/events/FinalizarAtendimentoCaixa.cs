using SimulacaoTrab1.Simulation.Model;
using SimulacaoTrab1.Simulation.Restaurant.entities;

namespace SimulacaoTrab1.Simulation.Restaurant.events
{
    public class FinalizarAtendimentoCaixa : Event
    {
        public FinalizarAtendimentoCaixa(string name, Resource cashier, EntitySet cashierQueue, Scheduler scheduler)
        : base(name, cashier, scheduler)
        {
            this.EntitySet = cashierQueue;
        }

        public override void Execute()
        {
            base.Execute();
            Resource.Release(1);

            ClientGroup clientGroup = (ClientGroup)EntitySet.Remove();

            if (clientGroup.Quantity == 1)
            {
                Scheduler.ScheduleNow(Scheduler.CreateEvent(new ChegadaBalcao("Desk Arrival", clientGroup, Scheduler)));
            }

            else if (clientGroup.Quantity == 2)
            {
                Scheduler.ScheduleNow(Scheduler.CreateEvent(new ChegadaMesaDoisLugares("2 Spots Arrival", clientGroup, Scheduler)));
            }

            else
            {
                Scheduler.ScheduleNow(Scheduler.CreateEvent(new ChegadaMesaQuatroLugares("4 Spots Arrival", clientGroup, Scheduler)));
            }

            Scheduler.ScheduleNow(Scheduler.CreateEvent(new InicioPreparoRefeicao("Begin Preparing Meal", clientGroup, Scheduler)));

            if (EntitySet.Entities.Count > 0)
            {
                Scheduler.ScheduleNow(Scheduler.CreateEvent(new AtendimentoCaixa("Service " + Resource.Name, Resource, EntitySet, Scheduler)));
            }
        }
    }
}
