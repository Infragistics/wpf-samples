using IgOutlook.Business.Contacts;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace IgOutlook.Services
{
    public interface IContactService
    {
        ObservableCollection<Contact> GetContacts();
        ObservableCollection<Contact> GetLocationContacts();
        ObservableCollection<string> GetContactsEmails();
        Dictionary<string, Contact> GetContactsAsDictionary();
        Contact GetContact(int id);
        ObservableCollection<Contact> GetContactFromEmails(List<string> emails);
        int GetNextValidContactId();
        void DeleteContact(Contact contact);
        void UpdateContact(Contact contact);
        void AddContact(Contact contact);
    }
}
