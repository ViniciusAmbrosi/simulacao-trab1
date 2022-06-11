﻿using SimulacaoTrab1.Simulation.Model;
using SimulacaoTrab1.Simulation.Restaurant.entities;

namespace SimulacaoTrab1.Simulation.Restaurant.events
{
    public class ChegadaBalcao : Event
    {
        public ClientGroup ClientGroup { get; set; }
        public EntitySet QueueFoodReady { get; set; }
        public EntitySet WaitDesk { get; set; }

        public ChegadaBalcao(string name, ClientGroup clientGroup, Scheduler scheduler)
        : base(name, scheduler)
        {
            this.ClientGroup = clientGroup;
            this.Resource = Scheduler.GetResourceByName("balcao");
            this.EntitySet = Scheduler.GetEntitySetByName("filaBalcao");
            this.QueueFoodReady = Scheduler.GetEntitySetByName("filaComidaPronta");
            this.WaitDesk = Scheduler.GetEntitySetByName("esperandoNoBalcao");
        }

        public new void Execute()
        {
            base.Execute();

            if (Resource.Allocate(1))
            {
                if (QueueFoodReady.GetById(ClientGroup.Id) != null)
                { // se refeicao ja ta pronta. Se não, TerminoPreparoRefeicao irá agendar
                    Scheduler.ScheduleNow(Scheduler.CreateEvent(new InicioRefeicao("Inicio Refeição", ClientGroup, Scheduler)));
                }
                else
                {
                    WaitDesk.Insert(ClientGroup);

                    Entity groupReady = WaitDesk.Entities.Find(groupWaiting => WaitDesk.GetById(groupWaiting.Id) != null);

                    if (groupReady != null)
                    {
                        Scheduler.ScheduleNow(Scheduler.CreateEvent(new InicioRefeicao("Inicio Refeição", (ClientGroup)groupReady, Scheduler)));
                    }
                }
            }
            else
            {
                EntitySet.Insert(ClientGroup);
            }

        }
    }
}
}