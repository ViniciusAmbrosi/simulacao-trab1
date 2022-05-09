namespace SimulacaoTrab1.Simulation.Model
{
    public class Event
    {
        public string Name { get; set; }
        //eventId: integer | atribuído pelo Scheduler
        public int? EventId { get; set; }

        public Event(string name)
        {
            this.Name = name;
        }
    }
}
