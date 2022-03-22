namespace SimulacaoTrab1
{
    public class WeightedArc : Arc
    {
        public WeightedArc(int ID, Position position, Transition transition, int weight = 1)
            : base(ID, position, transition, weight)
        {
        }
    }
}
