using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulacaoTrab1
{
    public class Transition
    {
        public List<Connector>? InboundConnectors { get; set; }
        public List<Connector>? OutboundConnectors { get; set; }
        public bool IsEnabled { get; set; }
        public int TransitionId { get; set; }

        public Transition(int transitionId,
            List<Connector>? inboundConnectors = null,
            List<Connector>? outboundConnectors = null,
            bool isEnabled = true)
        {
            this.InboundConnectors = inboundConnectors != null ? inboundConnectors : new List<Connector>();
            this.OutboundConnectors = outboundConnectors != null ? outboundConnectors : new List<Connector>();
            this.IsEnabled = isEnabled;
            this.TransitionId = transitionId;
        }

        public async Task<string> MoveAsync()
        {
            return await Move();
        }

        public void AttemptToEnableTransition()
        {
            if (InboundConnectors == null ||
                InboundConnectors.Any(connector => connector is BlockingConnector && ((BlockingConnector)connector).IsBlocked))
            {
                //skips in case of blocked or empty
                this.IsEnabled = false;
                return;
            }

            List<WeightedConnector> weightedInboundConnectors = GetWeightedConnectors(InboundConnectors);
            this.IsEnabled = weightedInboundConnectors.All(connector => connector.Position.MarkCounter >= connector.Weight);
        }

        //public Task<string> Move()
        //{
        //    if (InboundConnectors == null ||
        //        InboundConnectors.Any(connector => connector is BlockingConnector && ((BlockingConnector)connector).IsBlocked))
        //    {
        //        //skips in case of blocked or empty
        //        return Task.FromResult("Transition was not enabled");
        //    }

        //    List<WeightedConnector> weightedInboundConnectors = GetWeightedConnectors(InboundConnectors);

        //    if (weightedInboundConnectors.All(connector => connector.Position.MarkCounter >= connector.Weight))
        //    {
        //        //no blocking connectors + all weighted connectors eligible

        //        weightedInboundConnectors.ForEach(connector =>
        //        {
        //            //add sempahore eventually to handle race conditions
        //            connector.Position.MarkCounter -= connector.Weight;
        //        });

        //        List<WeightedConnector> weightedOutboundConnectors = GetWeightedConnectors(OutboundConnectors);

        //        weightedOutboundConnectors.ForEach(connector =>
        //        {
        //            connector.Position.MarkCounter += connector.Weight;
        //        });

        //        return Task.FromResult("Transition moved");
        //    }

        //    return Task.FromResult("Transition didn't move");
        //}        
        
        public Task<string> Move()
        {
            if (!IsEnabled) 
            {
                return Task.FromResult("Transition is not enabled");
            }

            List<WeightedConnector> weightedInboundConnectors = GetWeightedConnectors(InboundConnectors);

            if (weightedInboundConnectors.All(connector => connector.Position.MarkCounter >= connector.Weight))
            {
                //no blocking connectors + all weighted connectors eligible

                weightedInboundConnectors.ForEach(connector =>
                {
                    //add sempahore eventually to handle race conditions
                    connector.Position.MarkCounter -= connector.Weight;
                });

                List<WeightedConnector> weightedOutboundConnectors = GetWeightedConnectors(OutboundConnectors);

                weightedOutboundConnectors.ForEach(connector =>
                {
                    connector.Position.MarkCounter += connector.Weight;
                });

                return Task.FromResult("Transition moved");
            }

            return Task.FromResult("Transition didn't move");
        }

        private List<WeightedConnector> GetWeightedConnectors(List<Connector>? connectors)
        {
            return (connectors?.OfType<WeightedConnector>() ?? Enumerable.Empty<WeightedConnector>()).ToList();
        }
    }
}
