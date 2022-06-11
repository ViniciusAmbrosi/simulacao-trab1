using SimulacaoTrab1.Simulation.Model;

namespace SimulacaoTrab1.Simulation.Restaurant.events
{
    public class AtendimentoCaixa : Event
    {
        public AtendimentoCaixa(string name, Resource cashier, EntitySet cashierQueue, Scheduler scheduler)
        : base(name, cashier, scheduler)
        {
        }

        public new void Execute()
        {
            base.Execute();
            if (EntitySet.Entities.Count > 0)
            {

                if (Resource.Allocate(1))
                {
                    //conseguiu alocar caixa pra atender
                    //Agenda final do atendimento em normal (8,2) minutos
                    Scheduler.ScheduleIn(Scheduler.CreateEvent(new FinalizarAtendimentoCaixa("Finalizar atendimento caixa", Resource, EntitySet, Scheduler)), Scheduler.Normal(8, 2));
                }
            }
        }
    }
}
