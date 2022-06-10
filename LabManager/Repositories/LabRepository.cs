using LabManager.Models;
using Microsoft.Data.Sqlite;
using LabManager.Database;

namespace LabManager.Repositories;

class LabRepository
{
    private readonly DatabaseConfig _databaseConfig;

    public LabRepository(DatabaseConfig databaseConfig)
    {
        _databaseConfig = databaseConfig;
    }

    public List<Lab> GetAll()
    {
        var labs = new List<Lab>(); 
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Lab;";

        var reader = command.ExecuteReader();

        while (reader.Read())
        {
            var lab = ReaderToComputer(reader);
            labs.Add(lab);
        }

        reader.Close();
        connection.Close();

        return labs;
    }

    private Lab ReaderToLab(SqliteDataReader reader)
    {
        return new Lab(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2),  reader.GetString(3));
    }
}