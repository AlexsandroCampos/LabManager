using LabManager.Models;
using LabManager.Data;

namespace LabManager.Repositories;

class ComputerRepository
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
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("UPDATE Computers SET ram = @Ram, processor = @Processor WHERE id = @Id", computer);
  
        return this.GetById(computer.Id);
    }

    public bool ExistsById(int id) 
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var result = connection.ExecuteScalar<Boolean>("SELECT count(id) FROM Computers WHERE id = @Id", new { Id = id });

        return result;
    }
}