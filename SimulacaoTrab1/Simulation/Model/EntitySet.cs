namespace SimulacaoTrab1.Simulation.Model
{
    public class EntitySet
    {
        public string Name { get; set; }
        public int? Id { get; set; }
        public EntitySetMode EntitySetMode { get; set; }
        public int Size { get; set; }
        public int MaxPossibleSize { get; set; }
        public double LastLogTime { get; set; }
        public double TimeGap { get; set; }
        public bool IsLogging { get; set; }
        public double CreationTime { get; set; }
        public List<Entity> Entities { get; set; }
        public Scheduler Scheduler { get; set; }

        public IDictionary<double, int> Log = new Dictionary<double, int>();

        public IDictionary<int, double> EntitiesTimeInSet = new Dictionary<int, double>();

        public IDictionary<int, double> LastUpdateTime = new Dictionary<int, double>();

        public IDictionary<double, int> EntitiesSizeInTime = new Dictionary<double, int>();

        public EntitySet(string name, int maxPossibleSize, Scheduler scheduler)
        {
            this.Name = name;
            this.EntitySetMode = EntitySetMode.NONE;
            this.MaxPossibleSize = maxPossibleSize;
            this.Entities = new List<Entity>();
            this.Scheduler = scheduler;
            this.CreationTime = DateTime.Now.ToOADate();
        }

        public void Insert(Entity entity)
        {
            Scheduler.Log("\nInserindo entidade com id " + entity.Id + " e nome " + entity.Name + " na fila " + Name);
            if (Entities.Count >= MaxPossibleSize)
            {
                Scheduler.Log("\nNão foi possível inserir entidade com id " + entity.Id + " e nome " + entity.Name + " na fila " + Name + " pois a fila está cheia.");
            }
            else
            {
                Entities.Add(entity);
                List<EntitySet> entitySets = entity.Sets;
                entitySets.Add(this);
                entity.Sets = entitySets;
                LastUpdateTime.Add(entity.Id, Scheduler.Time);

                if (!EntitiesTimeInSet.ContainsKey(entity.Id))
                {
                    EntitiesTimeInSet.Add(entity.Id, 0.0);
                }
                UpdateEntitiesSizeInTime();
            }
            Scheduler.EnforceStepByStepExecution();
        }

        public Entity? Remove()
        {
            if (Entities.Count == 0)
            {
                return null;
            }

            Entity removed;

            switch(EntitySetMode)
            {
                case EntitySetMode.FIFO:
                    removed = Entities[0];
                    Entities.RemoveAt(0);
                    break;
                case EntitySetMode.LIFO:
                    removed = Entities[Entities.Count - 1];
                    Entities.RemoveAt(Entities.Count - 1);
                    break;
                case EntitySetMode.PRIORITY:
                    int index = GetIndexEntityMaxPriority();
                    removed = Entities[index];
                    Entities.RemoveAt(index);
                    break;
                default:
                    int random = new Random().Next(Entities.Count);
                    removed = Entities[random];
                    Entities.RemoveAt(random);
                    break;
            }

            Scheduler.Log("\nRemovendo entidade com id " + removed.Id + " e nome " + removed.Name + " da fila " + Name);
            Scheduler.EnforceStepByStepExecution();
            List<EntitySet> entitySets = removed.Sets;
            entitySets.Remove(this);
            removed.Sets = entitySets;
            UpdateEntitiesSizeInTime();
            UpdateEntitityTimeInSet(removed);
            return removed;
        }

        public int GetIndexEntityMaxPriority()
        {
            Entity entity = Entities.Aggregate((e1, e2) => e1.Priority >= e2.Priority ? e1 : e2);
            return Entities.IndexOf(entity);
        }

        public Entity? RemoveById(int id)
        {
            Entity? entity = Entities.Find(e => e.Id == id);

            if (entity == null)
            {
                Scheduler.Log("Entidade com id " + id + " não encontrada para remoção.");
                Scheduler.EnforceStepByStepExecution();
                return entity;
            }
            Scheduler.Log("\nRemovendo entidade com id " + entity.Id + " e nome " + entity.Name + " da fila " + Name);
            Scheduler.EnforceStepByStepExecution();
            Entities.Remove(entity);
            UpdateEntitiesSizeInTime();
            UpdateEntitityTimeInSet(entity);
            return entity;
        }

        public Entity? GetById(int id)
        {
            Entity? entity = Entities.Find(e => e.Id == id);
            return entity;
        }

        public void UpdateEntitityTimeInSet(Entity entity)
        {
            EntitiesTimeInSet[entity.Id] =  EntitiesTimeInSet[entity.Id] + (Scheduler.Time - LastUpdateTime[entity.Id]);
            LastUpdateTime[entity.Id] = Scheduler.Time;
        }

        public void UpdateEntitiesSizeInTime()
        {
            EntitiesSizeInTime[Scheduler.Time] = Entities.Count;
        }

        public double AverageSize()
        {
            int sumEntitiesSizeInSet = EntitiesSizeInTime.Sum(v => v.Value);
            if (EntitiesTimeInSet.Count > 0)
            {
                return sumEntitiesSizeInSet / EntitiesSizeInTime.Count;
            }
            return sumEntitiesSizeInSet;
        }

        public void LogTime()
        {
            while (shouldLogTime())
            {
                Log.Add(LastLogTime + TimeGap, Entities.Count);
                LastLogTime += TimeGap;
            }
        }

        public double GetSetTotalTime()
        {
            return Scheduler.Time - CreationTime;
        }

        public double AverageTimeInSet()
        {
            if (Entities.Count > 0)
            {
                foreach (var e in Entities)
                {
                    UpdateEntitityTimeInSet(e);
                }
            }

            double sumEntitiesTimeInSet = EntitiesTimeInSet.Sum(v => v.Value);

            if (EntitiesTimeInSet.Count > 0)
            {
                return sumEntitiesTimeInSet / EntitiesTimeInSet.Count;
            }
            return sumEntitiesTimeInSet;
        }

        public double MaxTimeInSet()
        {
            if (EntitiesTimeInSet.Count > 0)
            {
                return EntitiesTimeInSet.Values.Max();
            }
            return 0.0;
        }

        public void StartLog(double timeGap)
        {
            IsLogging = true;
            this.TimeGap = timeGap;
        }

        private bool shouldLogTime()
        {
            return IsLogging && LastLogTime + TimeGap <= Scheduler.Time;
        }
    }
}
