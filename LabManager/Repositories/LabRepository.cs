using LabManager.Models;
using LabManager.Data;
namespace LabManager.Repositories;

public class LabRepository
{
    private SystemContext _systemContext;

    public LabRepository(SystemContext systemContext)
    {
        _systemContext = systemContext;
    }

    public List<Lab> GetAll() => _systemContext.Labs.ToList();

    public void Save(Lab lab)
    {
        _systemContext.Add(lab);
        _systemContext.SaveChanges();
    }

    public Lab GetById(int id) => _systemContext.Labs.Find(id);

    public void Delete(int id)
    {
        var lab = GetById(id);
        _systemContext.Labs.Remove(lab);
        _systemContext.SaveChanges();
    }

    public Lab Update(Lab lab)
    {
        var labFound = GetById(lab.Id);

        labFound.Block = lab.Block;
        labFound.Name = lab.Name;
        labFound.Number = lab.Number;

       _systemContext.Labs.Update(labFound);
       _systemContext.SaveChanges();

        return labFound;
    }

    public bool ExistsById(int id) 
    {
        if(_systemContext.Labs.Find(id) == null)
        {
            return false;
        }

        return true;
    }
}