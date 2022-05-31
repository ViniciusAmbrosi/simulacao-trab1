namespace SimulacaoTrab1.Simulation.Model
{
    public class Event
    {
        public string Name { get; set; }

        public int? EventId { get; set; }

        public Event(string name)
        {
            this.Name = name;
        }
    }
}
