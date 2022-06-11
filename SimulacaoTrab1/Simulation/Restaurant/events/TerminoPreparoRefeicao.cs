﻿using SimulacaoTrab1.Simulation.Model;
using SimulacaoTrab1.Simulation.Restaurant.entities;

namespace SimulacaoTrab1.Simulation.Restaurant.events
{
    public class TerminoPreparoRefeicao : Event
    {
        public ClientGroup ClientGroup { get; set; }
        public EntitySet WaitDesk { get; set; }
        public EntitySet WaitM2 { get; set; }
        public EntitySet WaitM4 { get; set; }
        public EntitySet QueueKitchen { get; set; }

        public TerminoPreparoRefeicao(string name, ClientGroup clientGroup, Resource resource, Scheduler scheduler)
        : base(name, resource, scheduler)
        {
            this.ClientGroup = clientGroup;
            this.QueueKitchen = Scheduler.GetEntitySetByName("filaCozinha");
            this.EntitySet = Scheduler.GetEntitySetByName("filaComidaPronta");
            this.WaitDesk = Scheduler.GetEntitySetByName("esperandoNoBalcao");
            this.WaitM2 = Scheduler.GetEntitySetByName("esperandoM2");
            this.WaitM4 = Scheduler.GetEntitySetByName("esperandoM4");
        }

        public new void Execute()
        {
            base.Execute();
            Resource.Release(1);

            EntitySet.Insert(ClientGroup); // insere grupo na fila de comida pronta
            if (WaitDesk.GetById(ClientGroup.Id) != null)
            { 
                // verifica se o grupo já está sentado em algum lugar para iniciar a refeicao
                WaitDesk.RemoveById(ClientGroup.Id);
                Scheduler.ScheduleNow(Scheduler.CreateEvent(new InicioRefeicao("Inicio Refeição", ClientGroup, Scheduler)));
            }
            else if (WaitM2.GetById(ClientGroup.Id) != null)
            {
                WaitM2.RemoveById(ClientGroup.Id);
                Scheduler.ScheduleNow(Scheduler.CreateEvent(new InicioRefeicao("Inicio Refeição", ClientGroup, Scheduler)));
            }
            else if (WaitM4.GetById(ClientGroup.Id) != null)
            {
                WaitM4.RemoveById(ClientGroup.Id);
                Scheduler.ScheduleNow(Scheduler.CreateEvent(new InicioRefeicao("Inicio Refeição", ClientGroup, Scheduler)));
            }

            var nextOrder = QueueKitchen.Remove();
            if (nextOrder != null)
            {
                //agenda próximo preparo
                Scheduler.ScheduleNow(Scheduler.CreateEvent(new InicioPreparoRefeicao("Inicio Preparo Refeição", (ClientGroup)nextOrder, Scheduler)));
            }
        }
    }
}
