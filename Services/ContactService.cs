using Dapper;
using DapperDemo;
using DapperDemo.Context;
using Npgsql;

public class ContactService
{

    private DapperContext _context;
    public ContactService()
    {
        _context = new DapperContext();
    }
    public List<Contact> GetContacts()
    {
        var connection = _context.Connection;
        var sql = "SELECT id,name,phone FROM contacts";
        return connection.Query<Contact>(sql).ToList();
    }

    public List<Contact> GetContactsV2()
    {
        var sql = "select * from contacts";
        var contacts = new List<Contact>();
        var connection = _context.Connection;
        connection.Open();
        using (var command = new NpgsqlCommand(sql, connection))
        {
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    //map 
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


    public Contact AddContact(Contact contact)
    {
        // Add contact to database
        var connection = _context.Connection;
        string sql = $"insert into contacts (id,name,phone) values (@id,@name,@phone) returning id;";
        var createdId = connection.ExecuteScalar<int>(sql,contact);
        contact.Id = createdId;
        return contact;
    }

    public int UpdateContact(Contact contact)
    {
        using (NpgsqlConnection connection = _context.Connection)
        {
            string sql = "update contacts set name=@name, phone=@phone where id=@id;";
            var response = connection.Execute(sql,contact);
            return response;
        }
    }

    public int DeleteContact(int id)
    {
        var connection = _context.Connection;
        string sql = $"delete from contact where id=@id";
        var response = connection.Execute(sql, new { id });
        return response;
    }

    public int Count()
    {
        var connection = _context.Connection;
        var sql = "SELECT COUNT(*) FROM contacts";
        var count = connection.ExecuteScalar<int>(sql);

        return count;
    }

    public void ReadMultiple(int id)
    {
        string sql = @"
        SELECT * FROM contacts WHERE id = @id;
        SELECT * FROM contacts WHERE id > @id;
";
         var connection = _context.Connection;
        using (var multi = connection.QueryMultiple(sql, new { id }))
        {
            var invoice = multi.ReadFirst<Contact>();
            var invoiceItems = multi.Read<Contact>().ToList();
        }
    }


    public void SpecificColumn()
    {
        using (var connection = _context.Connection)
        {
            var sql = "SELECT  name, phone FROM contacts WHERE id = 1";
            var products = connection.Query<Contact>(sql);
	
          
        }
    }

    public Contact GetContactById(int id)
    {
        using (NpgsqlConnection connection = _context.Connection)
        {
            string sql = $"select * from contacts where id = @id";
            var response = connection.QuerySingleOrDefault<Contact>(sql, new { id });
            return response;
        }
    }
}