namespace SimulacaoTrab1.Simulation.Model
{
    public class EntitySet
    {
        public string Name { get; set; }
        public int? Id { get; set; }
        //mode: método remove() sorteia qual entidade será removida;
        //neste mode, é + interessante empregar removeById(id)
        //getMode(): mode e setMode(mode)
        public EntitySetMode EntitySetMode { get; set; }
        public int Size { get; set; }
        public int MaxPossibleSize { get; set; }
        public List<Entity> entities { get; set; }

        public EntitySet(string name, EntitySetMode entitySetMode, int maxPossibleSize)
        {
            this.Name = name;
            this.EntitySetMode = entitySetMode;
            this.MaxPossibleSize = maxPossibleSize;
            this.Size = 0;
            this.entities = new List<Entity>();
        }

        public void Push(Entity entity)
        {
            if (Size == 0 || Size + 1 <= MaxPossibleSize)
            {
                entities.Add(entity);
            }
        }

        public Entity? Pop()
        {
            if (EntitySetMode == EntitySetMode.FIFO)
            {
                return entities.First();
            }
            else if (EntitySetMode == EntitySetMode.LIFO)
            {
                return entities.Last();
            }
            else if (EntitySetMode == EntitySetMode.PRIORITY)
            {
                return entities.MinBy(x => x.Priority);
            }
            else 
            {
                return null;
            }
        }

        //removeById(id): Entity
        public Entity GetAndRemoveById(int id)
        {
            return null;
        }

        //isEmpty() : boolean 
        public bool IsEmpty()
        {
            return false;
        }

        //isFull() : boolean
        public bool IsFull()
        {
            return false;
        }

        //findEntity(id) : Entity
        //Retorna referência para uma Entity, se esta estiver presente nesta EntitySet
        public Entity FindEntity(int id)
        {
            return null;
        }

        //averageSize() : double | retorna quantidade média de entidades no conjunto
        public double AverageSize()
        {
            return 0;
        }

        //averageTimeInSet(): double | retorna tempo médio que as entidades permaneceram neste conjunto
        public double AverageTimeInSet()
        {
            return 0;
        }
        //maxTimeInSet(): double | retorna tempo mais longo que uma entidade permaneceu neste conjunto
        public double maxTimeInSet()
        {
            return 0;
        }

        //startLog(timeGap) | dispara a coleta(log) do tamanho do conjunto;
        //esta coleta é realizada a cada timeGap unidades de tempo
        public void StartLog(double timeGap)
        { 
        }

        //stopLog()
        public void StopLog()
        { 
        }

        //getLog()) | retorna uma lista contendo o log deste Resource até o momento;
        //cada elemento desta lista um par<tempoAbsoluto, tamanhoConjunto>
        public Dictionary<double, double> GetLog()
        {
            return null;
        }
    }
}
