namespace SimulacaoTrab1
{
    public class WeightedArc : Arc
    {
        public WeightedArc(int id, Place position, Transition transition, int weight = 1)
            : base(id, position, transition, weight)
        {
        }
    }
}
