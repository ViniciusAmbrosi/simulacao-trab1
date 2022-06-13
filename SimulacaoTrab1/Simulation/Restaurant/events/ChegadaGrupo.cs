using SimulacaoTrab1.Simulation.Model;
using SimulacaoTrab1.Simulation.Restaurant.entities;

namespace SimulacaoTrab1.Simulation.Restaurant.events
{
    public class ChegadaGrupo : Event
    {
        public EntitySet QueueFirstCashier { get; set; }

        public EntitySet QueueSecondCashier { get; set; }

        public Resource FirstCashier { get; set; }

        public Resource SecondCashier { get; set; }

        private readonly double THREE_HOURS_IN_SECONDS = 3 * 60 * 60;

        public ChegadaGrupo(string name, Scheduler scheduler)
        : base(name)
        {
            this.Scheduler = scheduler;

            this.QueueFirstCashier = Scheduler.GetEntitySetByName("filaCaixa1");
            this.QueueSecondCashier = Scheduler.GetEntitySetByName("filaCaixa2");
            this.FirstCashier = Scheduler.GetResourceByName("caixa1");
            this.SecondCashier = Scheduler.GetResourceByName("caixa2");
        }

        public override void Execute()
        {
            base.Execute();
            int quantity = new Random().Next(4) + 1;
            Entity clientGroup = Scheduler.CreateEntity(new ClientGroup("Group of " + quantity + " clientes", quantity, Scheduler));

            if (QueueFirstCashier.Entities.Count < QueueSecondCashier.Entities.Count)
            {
                QueueFirstCashier.Insert(clientGroup);
                Scheduler.ScheduleNow(Scheduler.CreateEvent(new AtendimentoCaixa("Service cashier 1", FirstCashier, QueueFirstCashier, Scheduler)));
            }
            else
            {
                QueueSecondCashier.Insert(clientGroup);
                Scheduler.ScheduleNow(Scheduler.CreateEvent(new AtendimentoCaixa("Service cashier 2", SecondCashier, QueueSecondCashier, Scheduler)));
            }

            if (Scheduler.Time < THREE_HOURS_IN_SECONDS)
            {
                double eventTime = 0;
                eventTime = Scheduler.Exponential(3);
                Scheduler.ScheduleIn(Scheduler.CreateEvent(new ChegadaGrupo("Group Arrival", Scheduler)), eventTime);
            }
        }
    }
}
