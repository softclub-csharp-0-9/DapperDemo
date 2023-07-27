using System.Data;
using Npgsql;

namespace DapperDemo.Services;

public class DatabaseService
{
    private string _server = "127.0.0.1";
    private string _port = "5432";
    private string _userId = "postgres";
    private string _password = "12345";
    private string _database = "dapper_demo_db";
    
    public void CreateDatabase()
    {
       
         var myConn = new NpgsqlConnection ($"Server={_server};Port={_port};User Id={_userId};Password={_password};");

        var str = $"create database {_database};";

        var myCommand = new NpgsqlCommand(str, myConn);
        try
        {
            myConn.Open();
            myCommand.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error on creating database");
        }
        finally
        {
            if (myConn.State == ConnectionState.Open)
            {
                myConn.Close();
            }
        }
    }
}