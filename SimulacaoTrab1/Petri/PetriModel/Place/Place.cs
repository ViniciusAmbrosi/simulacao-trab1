namespace SimulacaoTrab1
{
    public class Place
    {
        public int MarkCounter { get; set; }
        public int Id { get; set; }
        public string Label { get; set; }
        public int ReservedMarks { get; set; }
        public List<Arc> OutboundConnections { get; set; }
        public List<Action> DelayedActions { get; set; }

        public Place(int id, string label, int markCounter = 1)
        {
            this.MarkCounter = markCounter;
            this.ReservedMarks = 0;
            this.Id = id;
            this.Label = label;
            this.OutboundConnections = new List<Arc>();
            this.DelayedActions = new List<Action>();
        }

        public bool CanSupplyTransition(Arc arc)
        {
            if (this.OutboundConnections.Count > 1)
            {
                IEnumerable<WeightedArc> orderedArcs = GetWeightedArcsOrderedByPriority(this.OutboundConnections);
                int temporaryReservedMarks = 0;

                foreach (WeightedArc orderedArc in orderedArcs)
                {
                    int availableMarks = this.MarkCounter - temporaryReservedMarks;

                    if (availableMarks >= orderedArc.Weight)
                    {
                        //is prioritary arc and within mark bounds
                        if (orderedArc.Id.Equals(arc.Id))
                        {
                            return true;
                        }

                        temporaryReservedMarks += orderedArc.Weight;
                    }
                }

                //arc was left out due to prioritization, increasing priority
                DelayedActions.Add(() => ApplyPriorities(arc));

                return false;
            }

            //return simple mark comparation
            return this.MarkCounter >= arc.Weight;
        }

        public void ApplyPriorities(Arc arc)
        {
            arc.Priority += 1;
        }

        public IEnumerable<WeightedArc> GetWeightedArcsOrderedByPriority(List<Arc> arcs)
        {
            return arcs.OfType<WeightedArc>().OrderByDescending(arc => arc.Priority).ThenBy(arc => arc.Id) ?? Enumerable.Empty<WeightedArc>();
        }
    }
}
