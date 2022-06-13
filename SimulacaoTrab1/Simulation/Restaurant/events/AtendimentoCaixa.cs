using SimulacaoTrab1.Simulation.Model;

namespace SimulacaoTrab1.Simulation.Restaurant.events
{
    public class AtendimentoCaixa : Event
    {
        public AtendimentoCaixa(string name, Resource cashier, EntitySet cashierQueue, Scheduler scheduler)
        : base(name, cashier, scheduler)
        {
            this.EntitySet = cashierQueue;
        }

        public override void Execute()
        {
            base.Execute();
            if (EntitySet.Entities.Count > 0)
            {
                if (Resource.Allocate(1))
                {
                    Scheduler.ScheduleIn(
                        Scheduler.CreateEvent(new FinalizarAtendimentoCaixa("Finish cashier service", Resource, EntitySet, Scheduler)),
                        Scheduler.Normal(8, 2));
                }
            }
        }
    }
}
