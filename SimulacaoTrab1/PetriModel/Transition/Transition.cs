using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulacaoTrab1
{
    public class Transition
    {
        public List<Arc> InboundConnectors { get; set; }
        public List<Arc> OutboundConnectors { get; set; }
        public bool IsEnabled { get; set; }
        public int TransitionId { get; set; }
        public string Label { get; set; }

        public Transition(int transitionId,
            string label,
            List<Arc>? inboundConnectors = null,
            List<Arc>? outboundConnectors = null,
            bool isEnabled = true)
        {
            this.InboundConnectors = inboundConnectors ?? new List<Arc>();
            this.OutboundConnectors = outboundConnectors ?? new List<Arc>();
            this.IsEnabled = isEnabled;
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
                InboundConnectors.Any(connector => connector is BlockingArc && ((BlockingArc)connector).IsBlocked))
            {
                //skips in case of blocked or empty
                //handle starvation
                this.IsEnabled = false;
                return;
            }

            List<WeightedArc> weightedInboundConnectors = GetWeightedConnectors(InboundConnectors);
            this.IsEnabled = weightedInboundConnectors.All(connector => connector.Position.MarkCounter >= connector.Weight);
        }

        public Task<string> Move()
        {
            if (!IsEnabled)
            {
                return Task.FromResult("Transition is not enabled");
            }

            List<WeightedArc> weightedInboundConnectors = GetWeightedConnectors(InboundConnectors);

            if (weightedInboundConnectors.All(connector => connector.Position.MarkCounter >= connector.Weight))
            {
                //no blocking connectors + all weighted connectors eligible
                weightedInboundConnectors.ForEach(connector =>
                {
                    //add sempahore eventually to handle race conditions
                    connector.Position.MarkCounter -= connector.Weight;
                });

                List<WeightedArc> weightedOutboundConnectors = GetWeightedConnectors(OutboundConnectors);

                weightedOutboundConnectors.ForEach(connector =>
                {
                    connector.Position.MarkCounter += connector.Weight;
                });

                return Task.FromResult("Transition moved");
            }

            return Task.FromResult("Transition didn't move");
        }

        private List<WeightedArc> GetWeightedConnectors(List<Arc>? connectors)
        {
            return (connectors?.OfType<WeightedArc>() ?? Enumerable.Empty<WeightedArc>()).ToList();
        }
    }
}
