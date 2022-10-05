using Microsoft.EntityFrameworkCore;
using LabManager.Models;

namespace LabManager.Data;

public class SystemContext : DbContext 
{
    public DbSet<Computer> Computers { get; set; }
    public DbSet<Lab> Labs { get; set; }
    public SystemContext() { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySQL("server=localhost;database=estudante;user=root;password=senha;");
    }
}