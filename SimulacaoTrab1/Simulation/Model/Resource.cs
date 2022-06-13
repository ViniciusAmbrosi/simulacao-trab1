using System;

namespace SimulacaoTrab1.Simulation.Model

{
    public class Resource
    {
        public string Name;
        public int Id { get; set; }
        public int Quantity { get; set; }
        public Scheduler Scheduler { get; set; }
        public double TotalAllocationTime { get; set; }
        public int InitialQuantity { get; set; }

        public IDictionary<double, int> AllocatedResourcesOverTime;
        public Tuple<double, int> LastAllocation;

        public Resource(string name, int quantity)
        {
            this.Name = name;
            this.Quantity = quantity;
        }

        public Resource(String name, int quantity, Scheduler scheduler)
        {
            this.Name = name;
            this.Quantity = quantity;
            this.InitialQuantity = quantity;
            this.Scheduler = scheduler;
            this.AllocatedResourcesOverTime = new Dictionary<double, int>();
            AllocatedResourcesOverTime.Add(scheduler.Time, 0);
            LastAllocation = new Tuple<double, int>(scheduler.Time, InitialQuantity - this.Quantity);
            this.TotalAllocationTime = 0;
        }

        public bool Allocate(int quantity)
        {
            if (this.Quantity < quantity)
            {
                Scheduler.Log("Não foi possível alocar " + quantity + " quantidades do recurso " + Name + " pois não há a quantidade disponível.");
                Scheduler.EnforceStepByStepExecution();
                return false;
            }

            Scheduler.Log("Alocando " + quantity + " quantidades do recurso " + Name);
            Scheduler.EnforceStepByStepExecution();

            this.Quantity -= quantity;
            SaveAllocationStatistics();
            return true;
        }

        public void Release(int quantity)
        {
            Scheduler.Log("Liberando " + quantity + " quantidades do recurso " + Name);
            Scheduler.EnforceStepByStepExecution();

            this.Quantity += quantity;
            SaveAllocationStatistics();
        }

        private void SaveAllocationStatistics()
        {
            AllocatedResourcesOverTime[Scheduler.Time] = InitialQuantity - this.Quantity;
            if (LastAllocation.Item2 != 0)
            {
                TotalAllocationTime += Scheduler.Time - LastAllocation.Item1;
            }
            LastAllocation = new Tuple<double, int>(Scheduler.Time, InitialQuantity - this.Quantity);
        }

        public double AllocationRate()
        {
            if (AllocatedResourcesOverTime.Count == 1)
            {
                Console.WriteLine("Resource " + Name + " allocation rate: 0.0%");
                return 0.0;
            }

            SaveAllocationStatistics();

            double result = TotalAllocationTime / Scheduler.Time;
            Console.WriteLine("Resource " + Name + " allocation rate: " + new Decimal(result * 100).ToString("#.##") + "%");
            return result;
        }

        public double AverageAllocation()
        {
            double result = AllocatedResourcesOverTime.Values.Sum() / Scheduler.Time;
            Console.WriteLine("Resource " + Name + " average allocation: " + new Decimal(result * 100).ToString("#.##") + "%"););
            return result;
        }
    }
}
