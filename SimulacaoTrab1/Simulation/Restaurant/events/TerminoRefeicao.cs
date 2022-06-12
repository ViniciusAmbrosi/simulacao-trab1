using SimulacaoTrab1.Simulation.Model;
using SimulacaoTrab1.Simulation.Restaurant.entities;

namespace SimulacaoTrab1.Simulation.Restaurant.events
{
    public class TerminoRefeicao : Event
    {
        public ClientGroup ClientGroup { get; set; }

        public TerminoRefeicao(string name, ClientGroup clientGroup, Resource resource, Scheduler scheduler)
        : base(name, resource, scheduler)
        {
            this.ClientGroup = clientGroup;
        }

        public override void Execute()
        {
            base.Execute();

            Scheduler.ScheduleNow(Scheduler.CreateEvent(RemoveFromCorrectTable()));

        }

        private Event RemoveFromCorrectTable()
        {
            Event eventValue = null;
            if (this.ClientGroup.Quantity == 1)
            {
                eventValue = new SaidaBalcao("Leave Desk", ClientGroup, Resource, Scheduler);
            }
            else if (this.ClientGroup.Quantity == 2)
            {
                eventValue = new SaidaMesaDoisLugares("Leave 2 Spots Table", ClientGroup, Resource, Scheduler);
            }
            else
            {
                eventValue = new SaidaMesaQuatroLugares("Leave 4 Spots Table", ClientGroup, Resource, Scheduler);
            }
            return eventValue;
        }
    }
}
