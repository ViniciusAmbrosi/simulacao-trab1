using System;

namespace SimulacaoTrab1.Simulation.Model

{
    public class Resource
    {
        public string Name;
        public int Id { get; set; } // atribuído pelo Scheduler
        public int Quantity { get; set; } // quantidade de recursos disponíveis
        public Scheduler Scheduler { get; set; }
        public double TotalAllocationTime { get; set; }
        public IDictionary<double, int> AllocatedResourcesOverTime;
        public Tuple<double, int> LastAllocation;
        public int InitialQuantity { get; set; }

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
        { // true se conseguiu alocar os recursos
            if (this.Quantity < quantity)
            {
                Scheduler.Log("Não foi possível alocar " + quantity + " quantidades do recurso " + Name + " pois não há a quantidade disponível.");
                Scheduler.CheckStepByStepExecution();
                return false;
            }
            Scheduler.Log("Alocando " + quantity + " quantidades do recurso " + Name);
            Scheduler.CheckStepByStepExecution();
            this.Quantity -= quantity;
            SaveAllocationStatistics();
            return true;
        }

        public void Release(int quantity)
        {
            Scheduler.Log("Liberando " + quantity + " quantidades do recurso " + Name);
            Scheduler.CheckStepByStepExecution();
            this.Quantity += quantity;
            SaveAllocationStatistics();
        }

        private void SaveAllocationStatistics()
        {
            AllocatedResourcesOverTime.Add(Scheduler.Time, InitialQuantity - this.Quantity);
            if (LastAllocation.Item2 != 0)
            {
                TotalAllocationTime += Scheduler.Time - LastAllocation.Item1;
            }
            LastAllocation = new Tuple<double, int>(Scheduler.Time, InitialQuantity - this.Quantity);
        }

        // coleta de estatísticas

        public double AllocationRate()
        { // percentual do tempo (em relação ao tempo total simulado) em que estes recursos foram alocados

            //recursos nunca foram alocados
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
        { // quantidade média destes recursos que foram alocados (em relação ao tempo total simulado)
            double result = AllocatedResourcesOverTime.Values.Sum() / Scheduler.Time;
            Console.WriteLine("Resource " + Name + " average allocation: " + result);
            return result;
        }
    }
}
