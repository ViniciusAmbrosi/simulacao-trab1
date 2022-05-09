namespace SimulacaoTrab1.Simulation.Model
{
    public class Resource
    {
        public string Name { get; set; }
        //id: integer
        //atribuído pelo Scheduler
        public int Id { get; set; }
        //quantity: integer
        //quantidade de recursos disponíveis
        public int Quantity { get; set; }

        public Resource(string name, int quantity)
        {
            this.Name = name;
            this.Quantity = quantity;
        }

        //allocate(quantity): boolean | true se conseguiu alocar os recursos
        public bool Allocate(int quantity)
        {
            return false;
        }
        //release(quantity) - coleta de estatísticas
        public void Release(int quantity)
        {
        }

        //allocationRate(): double
        //percentual do tempo (em relação ao tempo total simulado) em que estes recursos foram alocados
        public double AllocationRate() 
        {
            return 0;
        }

        //averageAllocation(): double
        //quantidade média destes recursos que foram alocados (em relação ao tempo total simulado)
        public double AverageAllocation()
        {
            return 0;
        }
    }
}
