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
        public int Priority { get; set; }
        public int ReservedMarks { get; set; }

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
            this.Priority = 1;
            this.ReservedMarks = 0;
        }

        public async Task<string> MoveAsync()
        {
            return await Move();
        }

        public void AttemptToEnableTransition()
        {
            if (InboundConnectors == null ||
                InboundConnectors.Any(connector => connector is InhibitorArc && ((InhibitorArc)connector).IsBlocked))
            {
                //skips in case of blocked or empty
                //handle starvation
                this.IsEnabled = false;
                return;
            }

            List<WeightedArc> weightedInboundConnectors = GetWeightedConnectors(InboundConnectors);
            //pega o place 
            //ve se encontra mais de dois inbound do place para transitions
            //ve se tem arc com prioridade para place
            //ve se tem tokens pra suprir todos os arcos (4 tokens - 2 saidas - 2 tokens cada 0 PODE)
            //se não, escolhe maximo que consegue pagar
            //reserva tokens 
            //assigna prioridade para outras transitions

            this.IsEnabled = weightedInboundConnectors.All(connector => connector.Place.MarkCounter >= connector.Weight);
        }

        public Task<string> Move()
        {
            if (!IsEnabled)
            {
                return Task.FromResult("Transition is not enabled");
            }

            List<WeightedArc> weightedInboundConnectors = GetWeightedConnectors(InboundConnectors);

            if (weightedInboundConnectors.All(connector => connector.Transition.ReservedMarks > 0 || connector.Place.MarkCounter >= connector.Weight))
            {
                //no blocking connectors + all weighted connectors eligible
                weightedInboundConnectors.ForEach(connector =>
                {
                    //add sempahore eventually to handle race conditions
                    if (connector.Transition.ReservedMarks > 0)
                    {
                        connector.Place.MarkCounter -= connector.Transition.ReservedMarks;
                        connector.Place.ReservedMarks -= connector.Transition.ReservedMarks;
                    }
                    else
                    {
                        connector.Place.MarkCounter -= connector.Weight;
                    }
                });

                List<WeightedArc> weightedOutboundConnectors = GetWeightedConnectors(OutboundConnectors);

                weightedOutboundConnectors.ForEach(connector =>
                {
                    if (connector.Transition.ReservedMarks > 0)
                    {
                        connector.Place.MarkCounter += connector.Transition.ReservedMarks;
                        connector.Transition.ReservedMarks -= connector.Weight;
                    } else
                    {
                        connector.Place.MarkCounter += connector.Weight;
                    }
                });

                return Task.FromResult("Transition moved");
            }

            return Task.FromResult("Transition didn't move");
        }

        public List<WeightedArc> GetWeightedConnectors(List<Arc>? connectors)
        {
            return (connectors?.OfType<WeightedArc>() ?? Enumerable.Empty<WeightedArc>()).ToList();
        }
    }
}
