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
            //Grupo pode ser de 1 a 4 pessoas (sorteio randomico).
            int quantity = new Random().Next(4) + 1;
            Entity clientGroup = Scheduler.CreateEntity(new ClientGroup("Group of " + quantity + " clientes", quantity, Scheduler));

            //O grupo sempre escolhe a menor fila.
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

            //Gerar grupos de clientes por 3 horas.
            if (Scheduler.Time < THREE_HOURS_IN_SECONDS)
            {
                //A cada exponencial (3) minutos chega um grupo de clientes
                double eventTime = 0;
                eventTime = Scheduler.Exponential(3);
                Scheduler.ScheduleIn(Scheduler.CreateEvent(new ChegadaGrupo("Group Arrival", Scheduler)), eventTime);
            }

        }
    }
}
