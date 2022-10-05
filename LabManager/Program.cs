using LabManager.Repositories;
using LabManager.Models;
using LabManager.Data;

var systemContext = new SystemContext();
systemContext.Database.EnsureCreated();
var computerRepository = new ComputerRepository(systemContext);

// var labRepository = new LabRepository(systemContext);

// Routing
var modelName = args[0];
var modelAction = args[1];


if (modelName == "Computer")
{
    if (modelAction == "List")
    {
           foreach (var computer in computerRepository.GetAll())
           {
               Console.WriteLine($"{ computer.Id}, { computer.Ram}, {computer.Processor}");
           }

    }
    if (modelAction == "New")
    {
        computerRepository.Save(new Computer(Convert.ToInt32(args[2]), args[3], args[4]));
    }

    if(modelAction == "Show")
    {
        var id = Convert.ToInt32(args[2]);
        if(computerRepository.ExistsById(id)) 
        {
            var computer = computerRepository.GetById(id);
            Console.WriteLine($"{computer.Id}, {computer.Ram}, {computer.Processor}");
        }

        else 
        {
            Console.WriteLine($"O computador {id} não existe");
        }
        
    }

    if(modelAction == "Delete")
    {
        //if
        var id = Convert.ToInt32(args[2]);
        if(computerRepository.ExistsById(id))
        {
            computerRepository.Delete(id);
            Console.WriteLine("Completed computer deletion.");
        }

         else 
        {
            Console.WriteLine($"O computador {id} não existe");
        }
       
    }
    if (modelAction == "Update")
    {
        var id = Convert.ToInt32(args[2]);
        if(computerRepository.ExistsById(id))
        {
            var computer = computerRepository.Update(id, new Computer(Convert.ToInt32(args[2]), args[3], args[4]));
            Console.WriteLine($"Updated computer: {computer.Id}, {computer.Ram}, {computer.Processor}");
        }
        else
        {
            Console.WriteLine($"O computador {id} não existe");
        }
    }

}

// if (modelName == "Lab")
// {
//     if(modelAction == "List")
//     {
//         foreach (var lab in labRepository.GetAll())
//         {
//             Console.WriteLine($"{ lab.Id}, { lab.Number}, {lab.Name}, {lab.Block}");
//         }
//     }

//     if(modelAction == "New")
//     {
//         labRepository.Save(new Lab(Convert.ToInt32(args[2]), Convert.ToInt32(args[3]), args[4], args[5]));
//     }

//     if(modelAction == "Delete")
//     {
//         var id = Convert.ToInt32(args[2]);
//         if(labRepository.ExistsById(id))
//         {
//             labRepository.Delete(Convert.ToInt32(args[2]));
//             Console.WriteLine("Completed lab deletion.");
//         }
//         else
//         {
//             Console.WriteLine($"O Lab {id} não existe");
//         }
       
//     }

//     if(modelAction == "Show")
//     {
//         var id = Convert.ToInt32(args[2]);
//         if(labRepository.ExistsById(id))
//         {
//             var lab = labRepository.GetById(Convert.ToInt32(args[2]));
//             Console.WriteLine($"{ lab.Id}, { lab.Number}, {lab.Name}, {lab.Block}");
//         }
//         else
//         {
//             Console.WriteLine($"O Lab {id} não existe");
//         }
        
//     }

//     if(modelAction == "Update")
//     {
//         var id = Convert.ToInt32(args[2]);
//         if(labRepository.ExistsById(id))
//         {
//             var lab = labRepository.Update(new Lab(Convert.ToInt32(args[2]), Convert.ToInt32(args[3]), args[4], args[5]));
//             Console.WriteLine($"Updated lab: { lab.Id}, { lab.Number}, {lab.Name}, {lab.Block}");
//         }
//         else
//         {
//             Console.WriteLine($"O Lab {id} não existe");
//         }
        
//     }
// }

