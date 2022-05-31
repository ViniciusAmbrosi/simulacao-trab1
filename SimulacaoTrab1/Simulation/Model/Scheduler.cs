namespace SimulacaoTrab1.Simulation.Model
{
    public class Scheduler
    {
        //getTime()
        public double Time { get; set; }

        //scheduleNow(Event)
        public virtual void Schedule(Event eventToExecute)
        { 
        }
        //scheduleIn(Event, timeToEvent)
        public virtual void Schedule(Event eventToExecute, double timeToEvent)
        {
        }
        //scheduleAt(Event, absoluteTime)
        public virtual void Schedule(Event eventToExecute, DateTime absoluteTime)
        {
        }
        public void SimulateOneStep()
        {
        }
        //simulate
        //executa até esgotar o modelo, isto é, até a engine não ter mais nada para processar
        //(FEL vazia, i.e., lista de eventos futuros vazia)
        public virtual void Simulate()
        { 
        }
        public virtual void Simulate(double timeToStart)
        {
        }
        //simulateUntil(absoluteTime)
        //criação, destruição e acesso para componentes
        public virtual void Simulate(DateTime absoluteTime)
        {
        }
        //createEntity(Entity)
        //instancia nova Entity e destroyEntity(id)
        public void ReplaceEntity(Entity entity)
        { 
        }
        //getEntity(id): Entity
        //retorna referência para instância de Entity
        public Entity GetEntity(int id)
        {
            return null;
        }
        //createResource(name, quantity):id
        public int CreateResource(string name, int quantity)
        {
            return 0;
        }
        //getResource(id) : Resource  retorna referência para instância de Resource
        public Resource GetResource(int id)
        {
            return null;
        }
        //createProcess(name, duration): processId
        public int CreateProcess(string name, int duration)
        {
            return 0;
        }
        //getProcess(processId) :Process  retorna referência para instancia de Process
        public Process GetProcess(int id)
        {
            return null;
        }
        //createEvent(name): eventId
        public int CreateEvent(string name)
        {
            return 0;
        }
        //getEvent(eventId) :Event  retorna referência para instancia de Event
        public Event GetEvent(int id)
        {
            return null;
        }
        //createEntitySet(name, mode, maxPossibleSize): id
        public int CreateEntitySet(string name, EntitySetMode mode, int maxPossibleSize)
        {
            return 0;
        }
        //getEntitySet(id) : EntitySet  retorna referência para instancia de EntitySet random variates
        public EntitySet GetEntitySet(int id)
        {
            return null;
        }
        //uniform(minValue, maxValue): double
        public double Uniform(int minValue, int maxValue)
        {
            return 0;
        }
        //exponential(meanValue): double
        public double Exponential(int meanValue)
        {
            return 0;
        }
        //normal(meanValue, stdDeviationValue): double coleta de estatísticas
        public double Normal(int meanValue, int standardDeviationValue)
        {
            return 0;
        }
        //getEntityTotalQuantity(): integer  retorna quantidade de entidades criadas até o momento
        public int GetEntityTotalQuantity()
        {
            return 0;
        }
        //getEntityTotalQuantity(name): integer  retorna quantidade de entidades criadas cujo nome corresponde ao parâmetro, até o momento
        public int GetEntityTotalQuantity(string name)
        {
            return 0;
        }
        //averageTimeInModel(): double  retorna o tempo médio que as entidades permanecem no modelo, desde sua criação até sua destruição
        public double AverageTimeInModel()
        {
            return 0;
        }

        //maxEntitiesPresent():integer  retorna o número máximo de entidades presentes no modelo até o momento
        public int MaxEntitiesPresent()
        {
            return 0;
        }
    }
}
