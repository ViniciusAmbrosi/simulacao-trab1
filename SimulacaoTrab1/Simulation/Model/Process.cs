namespace SimulacaoTrab1.Simulation.Model
{
    public class Process
    {
        public string Name { get; set; }
        //processId: integer | atribuído pelo Scheduler
        public int? ProcessId { get; set; }
        //duration: double
        //é a duração(temporal) do processo;
        //seu valor é calculado no início da execução da execução desta instância;
        //getDuration(): double e setDuration(duration)
        public double Duration { get; set; }
        //isActive(): boolean
        //activate(boolean)
        public bool Active { get; set; }

        public Process(string name, double duration)
        {
            this.Name = name;
            this.Duration = duration;
        }
    }
}
