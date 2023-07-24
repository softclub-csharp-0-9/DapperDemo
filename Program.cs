var contactService = new ContactService();

var contacts = contactService.GetContacts();

foreach (var contact in contacts)
{
    Console.WriteLine(contact.Name);
}
