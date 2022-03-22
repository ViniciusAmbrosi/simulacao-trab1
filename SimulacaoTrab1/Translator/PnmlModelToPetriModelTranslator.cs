using SimulacaoTrab1.Reader.Model;

namespace SimulacaoTrab1.Translator
{
    public class PnmlModelToPetriModelTranslator
    {
        public static PetriNetwork GetTransitions(PNMLDocument document)
        {
            PetriNetwork petriNetwork = new PetriNetwork();

            foreach (var pnmlPlace in document.subnet.Places)
            {
                Place position = petriNetwork.createPosition(pnmlPlace.Id, pnmlPlace.Label, pnmlPlace.Tokens);
            }

            foreach (var pnmlTransition in document.subnet.Transitions)
            {
                Transition newTransition = petriNetwork.createTransition(pnmlTransition.id, pnmlTransition.Label);
            }

            int arcId = 0;
            foreach (var pnmlArc in document.subnet.Arcs)
            {
                Arc arc;
                if (petriNetwork.Positions.Any(position => position.Id == pnmlArc.sourceId))
                {
                    Place? position = petriNetwork.Positions.Find(position => position.Id == pnmlArc.sourceId);
                    Transition? transition = petriNetwork.Transitions.Find(transition => transition.TransitionId == pnmlArc.destinationid);

                    if (position == null || transition == null)
                    {
                        LogTranslationMalformed(pnmlArc, "Source is a position but there's no matching transition for destination");
                    }
                    else
                    {
                        arc = petriNetwork.createInboundConnection(arcId++, position, transition, pnmlArc.type == PNMLArcType.regular, pnmlArc.multiplicity);
                    }
                }
                else
                {
                    Transition? transition = petriNetwork.Transitions.Find(transition => transition.TransitionId == pnmlArc.sourceId);
                    Place? position = petriNetwork.Positions.Find(position => position.Id == pnmlArc.destinationid);

                    if (position == null || transition == null)
                    {
                        LogTranslationMalformed(pnmlArc, "Source is a transition but there's no matching position for destination");
                    }
                    else
                    {
                        arc = petriNetwork.createOutboundConnection(arcId++, position, transition, pnmlArc.type == PNMLArcType.regular, pnmlArc.multiplicity);
                    }
                }
            }

            return petriNetwork;
        }

        public static void LogTranslationMalformed(PNMLArc pnmlArc, string additionalInfo)
        {
            Console.WriteLine($"Cyclic arc identified. XML is malformed. \n" +
                $"Arc Information \n" +
                $"Type {pnmlArc.type} \n" +
                $"Multiplicity {pnmlArc.multiplicity} \n" +
                $"Source Id {pnmlArc.sourceId} \n" +
                $"Destination Id {pnmlArc.destinationid} \n" +
                $"Additional Information {additionalInfo} \n");
        }
    }
}
