using DapperDemo;

var contactService = new ContactService();

while (true)
{
    Console.WriteLine("\n Commands : \n r - all records \n c -for create \n u - for update");
    var command = Console.ReadLine();
    if (command == "r")
        Read();
    else if (command == "c")
        Create();
    else if (command == "u")
        Update();
}

void Read()
{
    var contacts = contactService.GetContacts();
    foreach (var contact in contacts)
    {
        Console.WriteLine($"id = {contact.Id}, name={contact.Name}, phone = {contact.Phone}");
    }
}

void Create()
{
    var contact = new Contact();
    Console.Write("Enter Id=");
    contact.Id = Convert.ToInt32(Console.ReadLine());
    Console.Write("Enter Name=");
    contact.Name = Console.ReadLine();
    Console.Write("Enter Phone=");
    contact.Phone = Console.ReadLine();

    contactService.AddContact(contact);
    
}


void Update()
{
    Console.Write("Enter the Id of the contanct : Id = ");
    var id = Convert.ToInt32(Console.ReadLine());

    var existing = contactService.GetContactById(id);
    Console.Write($"Name Before: {existing.Name} after : ");
    var inputName= Console.ReadLine();
    if (string.IsNullOrEmpty(inputName) == false)
        existing.Name = inputName;
    
    Console.Write($"Phone Before: {existing.Phone} after : ");
    var inputPhone= Console.ReadLine();
    if (string.IsNullOrEmpty(inputPhone) == false)
        existing.Phone = inputPhone;

    contactService.UpdateContact(existing);
    
}