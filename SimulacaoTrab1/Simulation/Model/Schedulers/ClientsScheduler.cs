namespace SimulacaoTrab1.Simulation.Model.Schedulers
{
    public class ClientsScheduler : Scheduler
    {
        public override void Simulate()
        {
        }

        public override void Simulate(double timeToStart)
        {
            var timeLimit = DateTime.Now.AddSeconds(timeToStart);
            Simulate(timeLimit);
        }

        public override void Simulate(DateTime absoluteTime)
        {
            while (DateTime.Now.ToOADate() < absoluteTime.ToOADate())
            {

            }
        }

        public override void Schedule(Event eventToExecute)
        {

        }

        public override void Schedule(Event eventToExecute, double timeToEvent)
        {
            
        }

        public override void Schedule(Event eventToExecute, DateTime absoluteTime)
        {

        }
    }
}
