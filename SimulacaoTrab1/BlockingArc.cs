namespace SimulacaoTrab1
{
    public class BlockingArc : Arc
    {
        public BlockingArc(int ID, bool isBlocked, Position position, Transition transition, int weight = 1) 
            : base(ID, position, transition, weight)
        {
            IsBlocked = isBlocked;
        }

        public bool IsBlocked { get; set; }
    }
}
