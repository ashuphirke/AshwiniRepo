using System;
namespace ContactManagementSystem
{
    public interface IContactRepository
    {
        int Add(Contact contact);
        Contact Get(string mobile);
        List<Contact> GetAll();
        int Update(Contact contact);
        int Delete(string mobile);
    }
}
