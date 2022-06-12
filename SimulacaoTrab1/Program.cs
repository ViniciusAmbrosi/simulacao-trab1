using SimulacaoTrab1.Simulation.Model;
using SimulacaoTrab1.Simulation.Restaurant.events;

Scheduler scheduler = new Scheduler();

scheduler.CreateEntitySet("filaCaixa1", new List<Entity>(), 1000).EntitySetMode = EntitySetMode.FIFO;
scheduler.CreateEntitySet("filaCaixa2", new List<Entity>(), 1000).EntitySetMode = EntitySetMode.FIFO;
scheduler.CreateEntitySet("filaMesa2Lugares", new List<Entity>(), 1000).EntitySetMode = EntitySetMode.FIFO;
scheduler.CreateEntitySet("filaMesa4Lugares", new List<Entity>(), 1000).EntitySetMode = EntitySetMode.FIFO;
scheduler.CreateEntitySet("filaBalcao", new List<Entity>(), 1000).EntitySetMode = EntitySetMode.FIFO;
scheduler.CreateEntitySet("esperandoNoBalcao", new List<Entity>(), 1000).EntitySetMode = EntitySetMode.FIFO;
scheduler.CreateEntitySet("esperandoM2", new List<Entity>(), 1000).EntitySetMode = EntitySetMode.FIFO;
scheduler.CreateEntitySet("esperandoM4", new List<Entity>(), 1000).EntitySetMode = EntitySetMode.FIFO;
scheduler.CreateEntitySet("filaCozinha", new List<Entity>(), 1000).EntitySetMode = EntitySetMode.FIFO;
scheduler.CreateEntitySet("filaComidaPronta", new List<Entity>(), 1000).EntitySetMode = EntitySetMode.FIFO;
scheduler.CreateResource("caixa1", 1);
scheduler.CreateResource("caixa2", 1);
scheduler.CreateResource("cozinha", 3);
scheduler.CreateResource("mesa2Lugares", 4);
scheduler.CreateResource("mesa4Lugares", 4);
scheduler.CreateResource("balcao", 6);
Event chegadaGrupo = scheduler.CreateEvent(new ChegadaGrupo("Group Arrival", scheduler));
scheduler.ScheduleNow(chegadaGrupo);


foreach (EntitySet es in scheduler.EntitySets)
{
    es.StartLog(14400);
}
    


//Simulate all
scheduler.Simulate();

//SimulateBy
//        while (scheduler.canExecute()) {
//            System.out.println("Digite por quanto tempo (em segundos) deseja executar o simulador: ");
//            String value = scanner.nextLine();
//            scheduler.simulateBy(Double.parseDouble(value));
//            scheduler.collectLogs();
//        }

//SimulateUntil
//        while (scheduler.canExecute()) {
//            System.out.println("Digite até quando (tempo absoluto, em segundos) deseja executar o simulador: ");
//            String value = scanner.nextLine();
//            scheduler.simulateUntil(Double.parseDouble(value));
//            scheduler.collectLogs();
//        }

//Step by step
//scheduler.SimulateStepByStep();

scheduler.CollectLogs();
