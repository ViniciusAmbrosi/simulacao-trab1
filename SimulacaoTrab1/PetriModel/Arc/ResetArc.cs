namespace SimulacaoTrab1
{
    public class ResetArc : WeightedArc
    {
        public ResetArc(int id, Place position, Transition transition, int weight = 1) : 
            base(id, position, transition, weight)
        {
        }
    }
}
