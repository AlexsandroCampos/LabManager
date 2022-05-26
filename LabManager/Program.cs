using Microsoft.Data.Sqlite;
using LabManager.Database;
using LabManager.Repositories;
using LabManager.Models;

var databaseConfig = new DatabaseConfig();

var databaseSetup = new DatabaseSetup(databaseConfig);

var computerRepository = new ComputerRepository(databaseConfig);

// Routing
var modelName = args[0];
var modelAction = args[1];


if (modelName == "Computer")
{
    if (modelAction == "List")
    {
           foreach (var computer in computerRepository.GetAll())
           {
               Console.WriteLine("{0}, {1}, {2}", computer.Id, computer.Ram, computer.Processor);
           }

    }
    if (modelAction == "New")
    {
        computerRepository.Save(new Computer(Convert.ToInt32(args[2]), args[3], args[4]));
    }

    if(modelAction == "Show")
    {
        var computer = computerRepository.GetById(Convert.ToInt32(args[2]));
        Console.WriteLine("{0}, {1}, {2}", computer.Id, computer.Ram, computer.Processor);
    }

    if(modelAction == "Delete")
    {
        computerRepository.Delete(Convert.ToInt32(args[2]));
        Console.WriteLine("Completed computer deletion.");
    }
}

