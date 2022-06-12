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
                //Se for grupo de 1 cliente, vai para o Balcão; se não houver banco disponível, aguarda na FilaBalc.
                Scheduler.ScheduleNow(Scheduler.CreateEvent(new ChegadaBalcao("Desk Arrival", clientGroup, Scheduler)));
            }

            else if (clientGroup.Quantity == 2)
            {
                Scheduler.ScheduleNow(Scheduler.CreateEvent(new ChegadaMesaDoisLugares("2 Spots Arrival", clientGroup, Scheduler)));
                //Se for de 2 a 4 clientes, vai para as mesas; grupo de 2 devem tentar sentar em mesas de 2 lugares.
                //Caso não hajam mesas disponíveis, o grupo aguarda em FilaMesas;
            }

            else
            {
                Scheduler.ScheduleNow(Scheduler.CreateEvent(new ChegadaMesaQuatroLugares("4 Spots Arrival", clientGroup, Scheduler)));
                //Grupos de 3 ou 4 devem tentar mesas de 4 lugares;
                //Caso não hajam mesas disponíveis, o grupo aguarda em FilaMesas;
            }

            Scheduler.ScheduleNow(Scheduler.CreateEvent(new InicioPreparoRefeicao("Begin Preparing Meal", clientGroup, Scheduler)));

            if (EntitySet.Entities.Count > 0)
            {
                Scheduler.ScheduleNow(Scheduler.CreateEvent(new AtendimentoCaixa("Service " + Resource.Name, Resource, EntitySet, Scheduler)));
            }
        }
    }
}
