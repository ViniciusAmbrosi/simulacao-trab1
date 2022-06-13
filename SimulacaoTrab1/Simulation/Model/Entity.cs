namespace SimulacaoTrab1.Simulation.Model
{
    public class Entity
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public double CreationTime{ get; set; }
        public int Priority { get; set; }
        public PetriNetwork? PetriNetwork { get; set; }
        public double DestructionTime { get; set; }

        public Scheduler Scheduler { get; set; }

        public List<EntitySet> Sets { get; set; }

        public Entity(string name, Scheduler scheduler)
        {
            this.Name = name;
            this.CreationTime = DateTime.Now.ToOADate();
            this.Scheduler = scheduler;
            this.Sets = new List<EntitySet>();
        }

        public Entity(string name, PetriNetwork petriNetwork, Scheduler scheduler) 
            : this(name, scheduler)
        {
            this.PetriNetwork = petriNetwork;
            this.Sets = new List<EntitySet>();
        }

        //getTimeSinceCreation(): double
        public double GetTimeSinceCreation()
        {
            return DateTime.Now.ToOADate() - this.CreationTime;
        }
    }
}
