namespace SimulacaoTrab1
{
    public class InhibitorArc : Arc
    {
        public InhibitorArc(int id, bool isBlocked, Place position, Transition transition, int weight = 1) 
            : base(id, position, transition, weight)
        {
        }

        public bool IsBlocked()
        {
            return this.Weight == Place.MarkCounter;
        }
    }
}
