using SimulacaoTrab1.PetriModel.Arc;
using SimulacaoTrab1.Reader.Model;

namespace SimulacaoTrab1.Translator
{
    public class PnmlModelToPetriModelTranslator
    {
        public static PetriNetwork GetPetryNetwork(PNMLDocument document)
        {
            Console.WriteLine("Loading petry nework from file into model");

            PetriNetwork petriNetwork = new PetriNetwork();

            Console.WriteLine("\nLoading places into model ----------------------------------------- ");
            foreach (var pnmlPlace in document.subnet.Places)
            {
                petriNetwork.createPosition(pnmlPlace.Id, pnmlPlace.Label, pnmlPlace.Tokens);
            }

            Console.WriteLine("\nLoading transitions into model ------------------------------------ ");
            foreach (var pnmlTransition in document.subnet.Transitions)
            {
                petriNetwork.createTransition(pnmlTransition.Id, pnmlTransition.Label);
            }

            int arcId = 0;
            Console.WriteLine("\nLoading arcs into model ------------------------------------------- ");
            foreach (var pnmlArc in document.subnet.Arcs)
            {
                ArcType arcType = (ArcType) Enum.Parse(typeof(ArcType), pnmlArc.type.ToString());

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
                        petriNetwork.createInboundConnection(arcId++, position, transition, arcType, pnmlArc.multiplicity);
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
                        petriNetwork.createOutboundConnection(arcId++, position, transition, arcType, pnmlArc.multiplicity);
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
