namespace SimulacaoTrab1.Simulation.Model
{
    public class Scheduler
    {
        public double Time { get; set; }
        public double TimeLimit { get; set; }
        public bool TimeLimitMode { get; set; }
        public bool StepByStepExecutionMode { get; set; }
        public List<Event> Events;
        public List<Resource> Resources;
        public List<EntitySet> EntitySets;
        public List<Entity> Entities;
        public List<Entity> DestroyedEntities;
        public int MaxEntities { get; set; }

        public int CurrentId = -1;

        public Scheduler()
        {
            Time = 0;
            TimeLimit = 0;
            TimeLimitMode = false;
            Events = new List<Event>();
            Resources = new List<Resource>();
            EntitySets = new List<EntitySet>();
            Entities = new List<Entity>();
            DestroyedEntities = new List<Entity>();
            StepByStepExecutionMode = false;
            MaxEntities = 0;
        }

        // disparo de eventos

        public void ScheduleNow(Event ev)
        {
            ev.Time = Time;
        }

        public void ScheduleIn(Event ev, double timeToEvent)
        {
            ev.Time = Time + timeToEvent;
        }

        public void ScheduleAt(Event ev, double absoluteTime)
        {
            ev.Time = absoluteTime;
        }

        public void WaitFor(double time)
        {
            this.Time += time;
        }

        //Próximo evento a ser executado
        public Event? GetNextEvent()
        {
            foreach (Event ev in GetEvents())
            {
                if (ev.Time >= Time)
                {
                    return ev;
                }
            }
            return null;
        }

        //Eventos sempre ordenados pelo tempo a ser executado
        public List<Event> GetEvents()
        {
            Events.Sort((e1, e2) => e1.Time.CompareTo(e2.Time));
            return Events.FindAll(e => !e.Executed);
        }

        // controlando tempo de execução

        public void Simulate()
        {
            while (GetNextEvent() != null)
            {
                Event? ev = GetNextEvent();
                Log("Iniciando execução do evento " + ev.Name);
                CheckStepByStepExecution();
                ExecuteEvent(ev);
            }
        }

        public void SimulateOneStep()
        {
            ExecuteEvent(GetNextEvent());
        }

        public void SimulateStepByStep()
        {
            StepByStepExecutionMode = true;
            Simulate();
        }

        public void CheckStepByStepExecution()
        {
            if (StepByStepExecutionMode)
            {
                Console.Write("Aperte \"ENTER\" para continuar...");
                var val = Console.ReadLine;
            }
        }

        public void simulateBy(double duration)
        {
            TimeLimitMode = true;
            double timeLimit = Time + duration;
            this.TimeLimit = timeLimit;

            while (canExecute() && Time < timeLimit)
            {
                ExecuteEvent(GetNextEvent());
            }
        }

        public Boolean canExecute()
        {
            return GetEvents().Count > 0;
        }

        protected void ExecuteEvent(Event ev)
        {

            foreach (EntitySet es in EntitySets)
            {
                es.LogTime();
            }


            if (TimeLimitMode && TimeLimit < ev.Time)
            {
                Time = TimeLimit;
            }
            else
            {
                Time = ev.Time;
                ev.Execute();
            }
        }

        public void SimulateUntil(double absoluteTime)
        {
            TimeLimit = absoluteTime;
            TimeLimitMode = true;
            while (canExecute() && Time < absoluteTime)
            {
                ExecuteEvent(GetNextEvent());
            }
        }

        // criação destruição e acesso para componentes

        public Entity CreateEntity(Entity entity)
        {
            entity.CreationTime = Time;
            entity.Scheduler = this;
            entity.Id = CurrentId++;
            Entities.Add(entity);
            if (Entities.Count > MaxEntities)
            {
                MaxEntities = Entities.Count;
            }
            Log("\nCriando entidade com nome: " + entity.Name + " e id " + entity.Id);
            CheckStepByStepExecution();
            return entity;
        }

        public void DestroyEntity(int id)
        {
            Entity entity = GetEntity(id);

            if (entity != null)
            {

                foreach (EntitySet es in EntitySets)
                {
                    es.RemoveById(id);
                }

                entity.DestructionTime = Time;
                Entities.Remove(entity);
                DestroyedEntities.Add(entity);
            }

        }

        public Entity? GetEntity(int id)
        {
            return Entities.Find(e => e.Id == id);
        }

        public Resource CreateResource(string name, int quantity)
        {
            Resource resource = new Resource(name, quantity, this);
            resource.Id = CurrentId++;
            Resources.Add(resource);

            Log("Criando recurso com nome: " + name + " e id " + resource.Id);
            CheckStepByStepExecution();

            return resource;
        }

        public Resource? getResource(int id)
        {
            return Resources.Find(r => r.Id == id);
        }

        public Resource? GetResourceByName(string name)
        {
            return Resources.Find(r => r.Name == name);
        }

        public Event CreateEvent(Event ev)
        {
            ev.Id = CurrentId++;
            Events.Add(ev);
            return ev;
        }

        public Event? GetEvent(int eventId)
        {
            return Events.Find(e => e.Id == eventId);
        }

        public EntitySet CreateEntitySet(string name, List<Entity> entities, int maxPossibleSize)
        {
            EntitySet entitySet = new EntitySet(name, maxPossibleSize, this);
            entitySet.Id = CurrentId++;
            EntitySets.Add(entitySet);
            Log("\nCriando entitySet com nome " + name + ", id " + entitySet.Id + " e tamanho " + maxPossibleSize);
            CheckStepByStepExecution();
            return entitySet;
        }

        public EntitySet? GetEntitySetByName(String name)
        {
            return EntitySets.Find(e => e.Name == name);
        }

        public EntitySet? GetEntitySet(int id)
        {
            return EntitySets.Find(es => es.Id == id);
        }

        // random variates

        public static double Uniform(double minValue, double maxValue)
        {
            double difference = maxValue - minValue;
            double res = minValue;
            res += new Random().NextDouble() * difference;
            return 60 * res;
        }

        public static double Exponential(double lambda)
        {
            Random rand = new Random();
            return 60 * Math.Log(1 - rand.NextDouble()) / (-lambda);
        }

        public static double Normal(double meanValue, double stdDeviationValue)
        {
            MathNet.Numerics.Distributions.Normal normalDist = new Normal(mean, stdDev);
            return 60 * 1;
        }

        // coleta de estatística

        public void Log(string message)
        {
            if (StepByStepExecutionMode)
            {
                Console.WriteLine(message);
            }
        }

        public void CollectLogs()
        {

            foreach (Resource r in Resources)
            {
                r.AllocationRate();
                r.AverageAllocation();
            }

            foreach (EntitySet es in EntitySets)
            {
                Console.WriteLine("\nSet: " + es.Name);

                foreach (KeyValuePair<double, int> pair in es.Log)
                {
                    Console.WriteLine("Time (in minutes): " + pair.Key / 60 + "; Quantity: " + pair.Value);
                }

                Console.WriteLine("Average size: " + es.AverageSize());
                Console.WriteLine("Average time in set: " + es.AverageTimeInSet() / 60);
                Console.WriteLine("Max time in set: " + es.MaxTimeInSet() / 60);
            }


            Console.WriteLine("\nAverage time in model: " + AverageTimeInModel() / 60);

            Console.WriteLine("Tempo atual: " + Time);

        }

        //Quantidade total de entidades que passaram pelo modelo
        public int GetEntityTotalQuantity()
        {
            return Entities.Count + DestroyedEntities.Count;
        }

        /**
         * retorna quantidade de entidades criadas cujo nome corresponde ao parâmetro, até o momento
         * @param name
         * @return
         */
        public int GetEntityTotalQuantity(string name)
        {
            return Entities.FindAll(e => e.Name == name).Count;
        }

        /**
         * retorna o tempo médio que as entidades permanecem no modelo, desde sua criação até sua destruição
         * @return
         */
        public double AverageTimeInModel()
        {
            double result = 0.0;

            if (Entities.Count > 0)
            {
                result += Entities.Sum(e => Time - e.CreationTime) / Entities.Count;
            }
            if (DestroyedEntities.Count > 0)
            {
                result += DestroyedEntities.Sum(e => e.DestructionTime - e.CreationTime) / DestroyedEntities.Count;
            }
            return result;
        }
    }
}
