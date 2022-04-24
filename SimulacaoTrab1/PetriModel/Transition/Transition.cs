using System.Collections.Generic;

namespace SimulacaoTrab1
{
    public class Transition
    {
        public List<Arc> InboundConnectors { get; set; }
        public List<Arc> OutboundConnectors { get; set; }
        public bool Enabled { get; set; }
        public int TransitionId { get; set; }
        public string Label { get; set; }

        public Transition(int transitionId,
            string label,
            List<Arc>? inboundConnectors = null,
            List<Arc>? outboundConnectors = null,
            bool isEnabled = false)
        {
            this.InboundConnectors = inboundConnectors ?? new List<Arc>();
            this.OutboundConnectors = outboundConnectors ?? new List<Arc>();
            this.Enabled = isEnabled;
            this.TransitionId = transitionId;
            this.Label = label;
        }

        public async Task<string> MoveAsync()
        {
            return await Move();
        }

        public void AttemptToEnableTransition()
        {
            if (InboundConnectors == null ||
                InboundConnectors.Any(
                    connector => connector is InhibitorArc &&
                    ((InhibitorArc)connector).IsBlocked()))
            {
                this.Enabled = false;
                return;
            }

            List<WeightedArc> weightedInboundArcs = GetWeightedArcs(InboundConnectors);
            bool isExecutionPossible = weightedInboundArcs.All(arc => arc.Place.CanSupplyTransition(arc));

            if (!isExecutionPossible)
            {
                //there's no reason to process if the inbound places / arcs do not meet base criteria for execution
                //execution could lead to deadlock where transitions allocate resources that won't be resolved in posterity
                this.Enabled = false;
                return;
            }

            else
            {
                this.Enabled = true;

                foreach (var arc in weightedInboundArcs)
                {
                    arc.Place.ReservedMarks = arc.Weight;
                }
            }
        }

        public Task<string> Move()
        {
            if (!Enabled)
            {
                return Task.FromResult("Transition is not enabled");
            }

            List<WeightedArc> weightedInboundConnectors = GetWeightedArcs(InboundConnectors);

            while (weightedInboundConnectors.All(connector => connector.Place.MarkCounter >= connector.Weight))
            {
                //no blocking connectors + all weighted connectors eligible
                //add sempahore eventually to handle race conditions
                weightedInboundConnectors.ForEach(connector =>
                {
                    if (connector is ResetArc)
                    {
                        connector.Place.MarkCounter -= connector.Place.MarkCounter;

                        if (connector.Place.ReservedMarks > 0)
                        {
                            connector.Place.ReservedMarks -= connector.Place.ReservedMarks;
                        }
                    }
                    else
                    {
                        connector.Place.MarkCounter -= connector.Weight;

                        if (connector.Place.ReservedMarks > 0)
                        {
                            connector.Place.ReservedMarks -= connector.Place.ReservedMarks;
                        }
                    }
                });

                List<WeightedArc> weightedOutboundConnectors = GetWeightedArcs(OutboundConnectors);

                weightedOutboundConnectors.ForEach(connector => connector.Place.MarkCounter += connector.Weight);
            }

            return Task.FromResult("Movement Processed");
        }

        public List<WeightedArc> GetWeightedArcs(List<Arc>? arcs)
        {
            return (arcs?.OfType<WeightedArc>() ?? Enumerable.Empty<WeightedArc>()).ToList();
        }

        public IEnumerable<WeightedArc> GetPriorityArcs(List<WeightedArc> arcs)
        {
            return arcs.Where(arc => arc.Priority != 0).OrderByDescending(arc => arc.Priority);
        }

        public IEnumerable<WeightedArc> GetUnprioritizedArcs(List<WeightedArc> arcs)
        {
            return arcs.Where(arc => arc.Priority == 0);
        }
    }
}
