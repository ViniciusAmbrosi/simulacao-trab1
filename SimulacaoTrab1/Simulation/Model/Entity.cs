namespace SimulacaoTrab1.Simulation.Model
{
    public class Entity
    {
        public string Name { get; set; }
        //id: integer atribuído pelo Scheduler
        //getId(): integer
        public int Id { get; set; }
        //creationTime: double atribuído pelo Scheduler
        public double CreationTime{ get; set; }
        //priority: integer sem prioridade: -1 (0: + alta e 255: + baixa)
        public int Priority { get; set; }
        //setPetriNet(PetriNet) e getPetriNet():PetriNet
        public PetriNetwork? PetriNetwork { get; set; }
        
        // atribuido pelo Scheduler
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
