using SimulacaoTrab1.Simulation.Model;
using SimulacaoTrab1.Simulation.Model.Entities;

EntitySet cashierEntitySet = new EntitySet("Cashiers", EntitySetMode.NONE, 2);

Resource cashiersQueue_1 = new Resource("Cashiers Queue 1", 1);
CashierEntity cashierEntity_1 = new CashierEntity("Cashier 1", null, cashiersQueue_1, 0);
cashierEntitySet.Push(cashierEntity_1);

Resource cashiersQueue_2 = new Resource("Cashiers Queue 2", 1);
CashierEntity cashierEntity_2 = new CashierEntity("Cashier 2", null, cashiersQueue_2, 0);
cashierEntitySet.Push(cashierEntity_2);

