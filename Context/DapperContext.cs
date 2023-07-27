using System.Data;
using Npgsql;

namespace DapperDemo.Context;

public class DapperContext
{
    private string _connectionString = "Server=localhost;Port=5432;Database=contactdb;User Id=postgres;Password=12345;";

    public NpgsqlConnection CreateConnection()
    {
        return new NpgsqlConnection(_connectionString);
    }

    public NpgsqlConnection Connection => new NpgsqlConnection(_connectionString);
}