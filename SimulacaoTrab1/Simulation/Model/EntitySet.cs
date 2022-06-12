namespace SimulacaoTrab1.Simulation.Model
{
    public class EntitySet
    {
        public string Name { get; set; }
        //id: integer | atribuído pelo Scheduler
        public int? Id { get; set; }

        //mode: método remove() sorteia qual entidade será removida;
        //neste mode, é + interessante empregar removeById(id)
        //getMode(): mode e setMode(mode)
        public EntitySetMode EntitySetMode { get; set; }
        //getSize() : integer | retorna quantidade de entidades presentes no conjunto no momento
        public int Size { get; set; }
        //maxPossibleSize: integer (zero for not size limited) | tamanho máximo que o conjunto pode ter
        //getMaxPossibleSize() : integer e setMaxPossibleSize(size)
        public int MaxPossibleSize { get; set; }

        public double LastLogTime { get; set; }

        public double TimeGap { get; set; }

        public IDictionary<double, int> Log = new Dictionary<double, int>();

        public bool IsLogging { get; set; }

        public double CreationTime { get; set; }

        public IDictionary<int, double> EntitiesTimeInSet = new Dictionary<int, double>();

        public IDictionary<int, double> LastUpdateTime = new Dictionary<int, double>();

        public IDictionary<double, int> EntitiesSizeInTime = new Dictionary<double, int>();

        public List<Entity> Entities { get; set; }

        public Scheduler Scheduler { get; set; }

        public EntitySet(string name, int maxPossibleSize, Scheduler scheduler)
        {
            this.Name = name;
            this.EntitySetMode = EntitySetMode.NONE;
            this.MaxPossibleSize = maxPossibleSize;
            this.Entities = new List<Entity>();
            this.Scheduler = scheduler;
            this.CreationTime = DateTime.Now.ToOADate();
        }

        //insert(Entity) | similar a enqueue ou push...
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
            Scheduler.CheckStepByStepExecution();
        }

        //remove(): Entity | similar a dequeue ou pop...
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
            Scheduler.CheckStepByStepExecution();
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
                Scheduler.CheckStepByStepExecution();
                return entity;
            }
            Scheduler.Log("\nRemovendo entidade com id " + entity.Id + " e nome " + entity.Name + " da fila " + Name);
            Scheduler.CheckStepByStepExecution();
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

        /**
        * Atualiza o tempo de permanência no set da entidade especificada no parâmetro
        */
        public void UpdateEntitityTimeInSet(Entity entity)
        {
            EntitiesTimeInSet[entity.Id] =  EntitiesTimeInSet[entity.Id] + (Scheduler.Time - LastUpdateTime[entity.Id]);
            LastUpdateTime[entity.Id] = Scheduler.Time;
        }

        /**
        * Atualiza o tamanho do set no tempo atual
        */
        public void UpdateEntitiesSizeInTime()
        {
            EntitiesSizeInTime[Scheduler.Time] = Entities.Count;
        }

        //Tamanho médio do set ao decorrer do tempo
        public double AverageSize()
        {
            int sumEntitiesSizeInSet = EntitiesSizeInTime.Sum(v => v.Value);
            if (EntitiesTimeInSet.Count > 0)
            {
                return sumEntitiesSizeInSet / EntitiesSizeInTime.Count;
            }
            return sumEntitiesSizeInSet;
        }

        //Faz o log da quantidade de entidades de x em x unidades de tempo
        public void LogTime()
        {
            while (shouldLogTime())
            {
                Log.Add(LastLogTime + TimeGap, Entities.Count);
                LastLogTime += TimeGap;
            }
        }

        //Tempo total do set desde sua criação
        public double GetSetTotalTime()
        {
            return Scheduler.Time - CreationTime;
        }

        //Média de tempo que as entidades ficam no set
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

        //Máximo de tempo que as entidades ficaram no set
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
