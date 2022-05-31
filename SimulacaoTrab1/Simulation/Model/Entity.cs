namespace SimulacaoTrab1.Simulation.Model
{
    public class Entity
    {
        public string Name { get; set; }
        //id: integer atribuído pelo Scheduler
        //getId(): integer
        public int? Id { get; set; }
        //creationTime: double atribuído pelo Scheduler
        public double CreationTime{ get; set; }
        //priority: integer sem prioridade: -1 (0: + alta e 255: + baixa)
        public int Priority { get; set; }

        public Resource? Resource { get; set; }

        public PetriNetwork? PetriNetwork { get; set; }

        public Entity(string name, int priority = -1)
        {
            this.Name = name;
            this.CreationTime = DateTime.Now.ToOADate();
            this.Priority = priority;
        }

        public Entity(string name, PetriNetwork? petriNetwork = null, Resource? resource = null, int priority = -1) 
            : this(name, priority)
        {
            this.Resource = resource;
            this.PetriNetwork = petriNetwork;
        }

        public double GetTimeSinceCreation()
        {
            return DateTime.Now.ToOADate() - this.CreationTime;
        }
    }
}
