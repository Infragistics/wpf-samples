using IgOutlook.Business.Contacts;
using IgOutlook.Services.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace IgOutlook.Services
{
    public class ContactService : IContactService
    {
        public ObservableCollection<Contact> GetContacts()
        {
            return Contacts;
        }

        public ObservableCollection<Contact> GetLocationContacts()
        {
            return new ObservableCollection<Contact>()
            {
               new Contact { Id = 1000, Email = "conferencerooma@demo.infragistics.com"},
               new Contact { Id = 1001, Email = "conferenceroomb@demo.infragistics.com"}
            };
        }
        public ObservableCollection<Contact> GetContactFromEmails(List<string> emails)
        {
            var result = new ObservableCollection<Contact>();

            foreach (var email in emails)
            {
                foreach (var contact in Contacts)
                {
                    if (contact.Email == email)
                        result.Add(contact);
                }
            }

            return result;
        }

        public Contact GetContact(int id)
        {
            return Contacts.FirstOrDefault(c => c.Id == id);
        }

        public void UpdateContact(Contact contact)
        {
            var originalContact = Contacts.FirstOrDefault(c => c.Id == contact.Id);
            if (originalContact != null)
                originalContact.CopyProperties(contact);
        }

        public void AddContact(Contact contact)
        {
            Contacts.Add(contact);
        }

        public void DeleteContact(Contact contact)
        {
            Contacts.Remove(contact);
        }

        public int GetNextValidContactId()
        {
            if (Contacts == null || Contacts.Count == 0)
            {
                return 1;
            }
            else
            {
                return Contacts.Select(c => c.Id).Max() + 1;
            }
        }

        public Dictionary<string, Contact> GetContactsAsDictionary()
        {
            var contactsDictionary = new Dictionary<string, Contact>();

            foreach (var contact in Contacts)
            {
                contactsDictionary.Add(contact.Email, contact);
            }

            return contactsDictionary;
        }

        public ObservableCollection<string> GetContactsEmails()
        {
            return new ObservableCollection<string>(contacts.Select(c => c.Email));
        }

        #region Data

        private static ObservableCollection<Contact> contacts;

        private static ObservableCollection<Contact> Contacts
        {
            get
            {
                if (contacts == null)
                {
                    contacts = new ObservableCollection<Contact>
                     {
                         new Contact
                         { 
                             Id = 1, 
                             FirstName = ServiceResources.Contact_DavidSmith_FirstName,
                             FileAs = ServiceResources.Contact_DavidSmith_FileAs,
                             PhotoUri = new Uri( "/IgOutlook.Modules.Contacts;component/Images/GUY01.jpg", UriKind.Relative), 
                             MiddleName = ServiceResources.Contact_DavidSmith_MiddleName,
                             LastName = ServiceResources.Contact_DavidSmith_LastName,
                             Company = ServiceResources.Contact_Company_Infragistics,
                             Department = ServiceResources.Contact_Department_HumanResources,
                             Email = "davids@demo.infragistics.com", 
                             JobTitle = ServiceResources.Contact_JobTitle_RecruitingDirector,
                             PhoneHome ="(214) 555-7222", 
                             Notes = ServiceResources.Contact_Notes
                         },

                        new Contact
                         { 
                             Id = 2, 
                             FirstName = ServiceResources.Contact_BobbyJones_FirstName, 
                             FileAs = ServiceResources.Contact_BobbyJones_FileAs, 
                             PhotoUri = new Uri("/IgOutlook.Modules.Contacts;component/Images/GUY02.jpg", UriKind.Relative), 
                             MiddleName = ServiceResources.Contact_BobbyJones_MiddleName, 
                             LastName = ServiceResources.Contact_BobbyJones_LastName,
                             Company = ServiceResources.Contact_Company_Infragistics,
                             Department = ServiceResources.Contact_Department_Development,
                             Email = "bobbyj@demo.infragistics.com", 
                             JobTitle = ServiceResources.Contact_JobTitle_SoftwareEngineer,
                             PhoneHome ="(212) 555-9938", 
                             Notes = ServiceResources.Contact_Notes 
                         },

                         new Contact
                         { 
                             Id = 3, 
                             FirstName = ServiceResources.Contact_JonathanBarclay_FirstName, 
                             FileAs =  ServiceResources.Contact_JonathanBarclay_FileAs,
                             PhotoUri = new Uri( "/IgOutlook.Modules.Contacts;component/Images/GUY03.jpg", UriKind.Relative), 
                             MiddleName = ServiceResources.Contact_JonathanBarclay_MiddleName,  
                             LastName= ServiceResources.Contact_JonathanBarclay_LastName,
                             Company = ServiceResources.Contact_Company_Infragistics,
                             Department = ServiceResources.Contact_Department_Development,
                             Email = "jonathanb@demo.infragistics.com",  
                             JobTitle = ServiceResources.Contact_JobTitle_SoftwareEngineer,
                             PhoneHome ="(212) 555-9911", 
                             Notes = ServiceResources.Contact_Notes
                         },

                        new Contact
                         { 
                             Id = 4, 
                             FirstName = ServiceResources.Contact_BradleySwinford_FirstName, 
                             FileAs = ServiceResources.Contact_BradleySwinford_FileAs,
                             PhotoUri = new Uri( "/IgOutlook.Modules.Contacts;component/Images/GUY04.jpg", UriKind.Relative), 
                             MiddleName = ServiceResources.Contact_BradleySwinford_MiddleName,
                             LastName = ServiceResources.Contact_BradleySwinford_LastName,
                             Company = ServiceResources.Contact_Company_Infragistics,
                             Department = ServiceResources.Contact_Department_Development, 
                             Email = "bradleys@demo.infragistics.com",  
                             JobTitle = ServiceResources.Contact_JobTitle_SoftwareEngineer,
                             PhoneHome ="(516) 551-9887", 
                             Notes = ServiceResources.Contact_Notes 
                         },

                        new Contact
                         { 
                             Id = 5, 
                             FirstName = ServiceResources.Contact_DerekHardstone_FirstName,
                             FileAs = ServiceResources.Contact_DerekHardstone_FileAs,
                             PhotoUri = new Uri( "/IgOutlook.Modules.Contacts;component/Images/GUY05.jpg", UriKind.Relative), 
                             MiddleName = ServiceResources.Contact_DerekHardstone_MiddleName,
                             LastName = ServiceResources.Contact_DerekHardstone_LastName,
                             Company = ServiceResources.Contact_Company_Infragistics,
                             Department = ServiceResources.Contact_Department_Development,
                             Email = "derekh@demo.infragistics.com",  
                             JobTitle = ServiceResources.Contact_JobTitle_SoftwareEngineer,
                             PhoneHome ="(211) 255-3905", 
                             Notes = ServiceResources.Contact_Notes 
                         },

                        new Contact
                         { 
                             Id = 6, 
                             FirstName = ServiceResources.Contact_MaryHernandez_FirstName,
                             FileAs = ServiceResources.Contact_MaryHernandez_FileAs,
                             PhotoUri = new Uri( "/IgOutlook.Modules.Contacts;component/Images/GIRL01.jpg", UriKind.Relative), 
                             MiddleName = ServiceResources.Contact_MaryHernandez_MiddleName,
                             LastName = ServiceResources.Contact_MaryHernandez_LastName, 
                             Company = ServiceResources.Contact_Company_Infragistics,
                             Department = ServiceResources.Contact_Department_VisualDesign,
                             Email = "maryh@demo.infragistics.com", 
                             JobTitle = ServiceResources.Contact_JobTitle_VisualDesigner,
                             PhoneHome ="(211) 555-8275", 
                             Notes = ServiceResources.Contact_Notes 
                         },

                        new Contact
                         { 
                             Id = 7, 
                             FirstName = ServiceResources.Contact_BarbaraBailey_FirstName,
                             FileAs = ServiceResources.Contact_BarbaraBailey_FileAs,
                             PhotoUri = new Uri( "/IgOutlook.Modules.Contacts;component/Images/GIRL02.jpg", UriKind.Relative), 
                             MiddleName = ServiceResources.Contact_BarbaraBailey_MiddleName, 
                             LastName = ServiceResources.Contact_BarbaraBailey_LastName,
                             Company = ServiceResources.Contact_Company_Infragistics, 
                             Department = ServiceResources.Contact_Department_HumanResources,
                             Email = "barbarab@demo.infragistics.com",  
                             JobTitle = ServiceResources.Contact_JobTitle_Hr,
                             PhoneHome ="(512) 555-3899", 
                             Notes = ServiceResources.Contact_Notes 
                         },

                        new Contact
                         { 
                             Id = 8, 
                             FirstName = ServiceResources.Contact_MargaretJones_FirstName,
                             FileAs = ServiceResources.Contact_MargaretJones_FileAs,
                             PhotoUri = new Uri( "/IgOutlook.Modules.Contacts;component/Images/GIRL03.jpg", UriKind.Relative), 
                             MiddleName = ServiceResources.Contact_MargaretJones_MiddleName,
                             LastName = ServiceResources.Contact_MargaretJones_LastName,
                             Company = ServiceResources.Contact_Company_Infragistics,
                             Department = ServiceResources.Contact_Department_VisualDesign,
                             Email = "margaretj@demo.infragistics.com", 
                             JobTitle = ServiceResources.Contact_JobTitle_VisualDesigner,
                             PhoneHome ="(512) 555-7776", 
                             Notes = ServiceResources.Contact_Notes 
                         },

                        new Contact
                         { 
                             Id = 9, 
                             FirstName = ServiceResources.Contact_JaneMeadows_FirstName,
                             FileAs = ServiceResources.Contact_JaneMeadows_FileAs,
                             PhotoUri = new Uri( "/IgOutlook.Modules.Contacts;component/Images/GIRL04.jpg", UriKind.Relative), 
                             MiddleName = ServiceResources.Contact_JaneMeadows_MiddleName,
                             LastName = ServiceResources.Contact_JaneMeadows_LastName,
                             Company = ServiceResources.Contact_Company_Infragistics,
                             Department = ServiceResources.Contact_Department_HumanResources,
                             Email = "janem@demo.infragistics.com", 
                             JobTitle = ServiceResources.Contact_JobTitle_Hr,
                             PhoneHome ="(213) 555-8837", 
                             Notes = ServiceResources.Contact_Notes 
                         },

                        new Contact
                         { 
                             Id = 10, 
                             FirstName = ServiceResources.Contact_SusanSpringfield_FirstName,
                             FileAs = ServiceResources.Contact_SusanSpringfield_FileAs,
                             PhotoUri = new Uri( "/IgOutlook.Modules.Contacts;component/Images/GIRL05.jpg", UriKind.Relative), 
                             MiddleName = ServiceResources.Contact_SusanSpringfield_MiddleName,
                             LastName = ServiceResources.Contact_SusanSpringfield_LastName,
                             Company = ServiceResources.Contact_Company_Infragistics,
                             Department = ServiceResources.Contact_Department_Sales,
                             Email = "susans@demo.infragistics.com", 
                             JobTitle = ServiceResources.Contact_Department_DirectorOfSales,
                             PhoneHome ="(533) 555-1424", 
                             Notes = ServiceResources.Contact_Notes 
                         }
                     };
                }

                return contacts;
            }
        }

        #endregion //Data
    }
}
