namespace SimulacaoTrab1
{
    public class BlockingArc : Arc
    {
        public BlockingArc(int id, bool isBlocked, Place position, Transition transition, int weight = 1) 
            : base(id, position, transition, weight)
        {
            IsBlocked = isBlocked;
        }

        public bool IsBlocked { get; set; }
    }
}
