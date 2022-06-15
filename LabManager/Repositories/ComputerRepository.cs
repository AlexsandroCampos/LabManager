using LabManager.Models;
using Microsoft.Data.Sqlite;
using LabManager.Database;
using Dapper;

namespace LabManager.Repositories;

class ComputerRepository
{
    private readonly DatabaseConfig _databaseConfig;

    public ComputerRepository(DatabaseConfig databaseConfig)
    {
        _databaseConfig = databaseConfig;
    }
    public List<Computer> GetAll() 
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var computers = connection.Query<Computer>("SELECT * FROM Computers").ToList();

        return computers;
    }

    public void Save(Computer computer)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("INSERT INTO Computers VALUES(@Id, @Ram, @Processor)", computer);
    }

    public Computer GetById(int id)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Computers WHERE id = $id;";
        command.Parameters.AddWithValue("$id", id);

        var reader = command.ExecuteReader();
        reader.Read();
        var computer = ReaderToComputer(reader);

        reader.Close();
        connection.Close(); 

        return computer;
    }

    public void Delete(int id)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM Computers WHERE id = $id;";
        command.Parameters.AddWithValue("$id", id);
        command.ExecuteNonQuery();

        connection.Close();
    }

    public Computer Update(Computer computer)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "UPDATE Computers SET ram = $ram, processor = $processor WHERE id = $id;";
        command.Parameters.AddWithValue("$id", computer.Id);
        command.Parameters.AddWithValue("$ram", computer.Ram);
        command.Parameters.AddWithValue("$processor", computer.Processor);
        command.ExecuteNonQuery();
        connection.Close();

        return this.GetById(computer.Id);
    }

    public bool ExistsById(int id) 
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT count(id) FROM Computers WHERE id = $id;";
        command.Parameters.AddWithValue("$id", id);

        // var reader = command.ExecuteReader();
        // reader.Read();
        // var result = reader.GetBoolean(0);

        var result = Convert.ToBoolean(command.ExecuteScalar());

        connection.Close();
        return result;
    }

    private Computer ReaderToComputer(SqliteDataReader reader) 
    {
        return new Computer( reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
    }
}