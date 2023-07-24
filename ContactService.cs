using Dapper;
using DapperDemo;
using Npgsql;

public class ContactService
{
    private string _connectionString = "Server=localhost;Port=5432;Database=contactdb;User Id=postgres;Password=12345;";

    public List<Contact> GetContacts()
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            return  connection.Query<Contact>("SELECT * FROM contacts").ToList();
        }
    }

    public List<Contact> GetContactsV2()
    {
        var sql = "select * from contacts";
        var contacts = new List<Contact>();
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();
            using (var command = new NpgsqlCommand(sql, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    var product = new Contact()
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        Name = reader.GetString(reader.GetOrdinal("Name")),
                        Phone = reader.GetString(reader.GetOrdinal("Phone")),
                    };
                    contacts.Add(product);
                }
            }
        }

        return contacts;
    }









    public int AddContact(Contact contact)
    {
        // Add contact to database
        using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
        {
            string sql = $"INSERT INTO contact (name, phone) " +
                         $"VALUES (" +
                         $"'{contact.Name}', " +
                         $"'{contact.Phone}'";
            var response = connection.Execute(sql);

            return response;
        }
    }

    public int UpdateContact(Contact contact)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
        {
            string sql = $"update contact set " +
                         $"name = '{contact.Name}', " +
                         $"phone='{contact.Phone}', " +
                         $"where id = {contact.Id};";
            var response = connection.Execute(sql);
            return response;
        }
    }

    public int DeleteContact(int id)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
        {
            string sql = $"delete from contact where id = {id}";
            var response = connection.Execute(sql);
            return response;
        }
    }

    public Contact GetContactById(string id)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
        {
            string sql = $"select * from contact where id = @id";
            var response = connection.QuerySingleOrDefault<Contact>(sql, new { id });
            return response;
        }
    }
}