namespace SimulacaoTrab1.Simulation.Model
{
    public class Process
    {
        public string Name { get; set; }
        public int? ProcessId { get; set; }
        public double Duration { get; set; }
        public bool Active { get; set; }

        public Process(string name, double duration)
        {
            this.Name = name;
            this.Duration = duration;
        }
    }
}
