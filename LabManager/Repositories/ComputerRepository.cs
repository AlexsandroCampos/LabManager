using LabManager.Models;
using LabManager.Data;

namespace LabManager.Repositories;

public class ComputerRepository
{
    private SystemContext _systemContext;

    public ComputerRepository(SystemContext systemContext)
    {
        _systemContext = systemContext;
    }
    public List<Computer> GetAll() => _systemContext.Computers.ToList();

    public void Save(Computer computer)
    {
        _systemContext.Computers.Add(computer);
        _systemContext.SaveChanges();
    }

    public Computer GetById(int id) => _systemContext.Computers.Find(id);

    public void Delete(int id)
    {
        var computer = GetById(id);
        _systemContext.Computers.Remove(computer);
        _systemContext.SaveChanges();
    }

    public Computer Update(Computer computer)
    {
        var computerFound = GetById(computer.Id);
        computerFound.Ram = computer.Ram;
        computerFound.Processor = computer.Processor;
        _systemContext.Computers.Update(computerFound);
        _systemContext.SaveChanges();
        return computerFound;
    }

    public bool ExistsById(int id) 
    {
        if(_systemContext.Computers.Find(id) == null)
        {
            return false;
        }

        return true;
    } 
}