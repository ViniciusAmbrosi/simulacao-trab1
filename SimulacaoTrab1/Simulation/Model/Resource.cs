namespace SimulacaoTrab1.Simulation.Model
{
    public class Resource
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public int Quantity { get; set; }

        public Resource(string name, int quantity)
        {
            this.Name = name;
            this.Quantity = quantity;
        }

        public bool Allocate(int quantity)
        {
            if (Quantity > 0)
            {
                Quantity++;
                return true;
            }

            return false;
        }

        public void Release(int quantity)
        {
            Quantity--;
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
