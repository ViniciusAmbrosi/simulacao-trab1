namespace SimulacaoTrab1.Simulation.Model
{
    public class Event
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public double Time { get; set; }
        public Scheduler Scheduler { get; set; }
        public EntitySet EntitySet { get; set; }
        public Resource Resource { get; set; }
        public bool Executed { get; set; }

        public Event(string name)
        {
            this.Name = name;
        }

        public Event(string name, Scheduler scheduler)
        {
            this.Name = name;
            this.Scheduler = scheduler;
        }

        public Event(string name, Resource resource, Scheduler scheduler)
        {
            this.Name = name;
            this.Resource = resource;
            this.Scheduler = scheduler;
        }

        public virtual void Execute()
        {
            this.Executed = true;
        }
    }
}
