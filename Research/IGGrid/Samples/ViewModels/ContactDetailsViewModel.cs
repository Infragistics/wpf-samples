using System;
using System.Collections.ObjectModel;
using IGGrid.Models;
using IGGrid.Resources;

namespace IGGrid.ViewModels
{
    public class ContactDetailsViewModel
    {
        public ContactDetailsViewModel() 
        {
            // Load sample data
            this.ContactsDetails = LoadContactsDetails(); 
        }

        #region ContactsDetails Member
        private ObservableCollection<ContactDetails> _contactsDetails = new ObservableCollection<ContactDetails>();
        public ObservableCollection<ContactDetails> ContactsDetails { get; set; }
        #endregion

        public ObservableCollection<ContactDetails> LoadContactsDetails()
        {
            ObservableCollection<ContactDetails> collection = new ObservableCollection<ContactDetails>();
            
            collection.Add(new ContactDetails
            {
                ContactInfo = new Coworker 
                { 
                    FullName = GridStrings.XG_ImplicitDataTemplates_Coworker1_FN,
                    NickName = GridStrings.XG_ImplicitDataTemplates_Coworker1_SN,
                    Department = GridStrings.XG_ImplicitDataTemplates_Dept1
                },
                Birthday = new DateTime(1978, 6, 30),
                Reminder = true
            });

            collection.Add(new ContactDetails
            {
                ContactInfo = new Coworker 
                {
                    FullName = GridStrings.XG_ImplicitDataTemplates_Coworker2_FN,
                    NickName = GridStrings.XG_ImplicitDataTemplates_Coworker2_SN,
                    Department = GridStrings.XG_ImplicitDataTemplates_Dept2
                },
                Birthday = new DateTime(1980, 3, 3),
                Reminder = false
            });

            collection.Add(new ContactDetails
            {                
                ContactInfo = new Coworker 
                {
                    FullName = GridStrings.XG_ImplicitDataTemplates_Coworker3_FN,
                    NickName = GridStrings.XG_ImplicitDataTemplates_Coworker3_SN,
                    Department = GridStrings.XG_ImplicitDataTemplates_Dept3
                },
                Birthday = new DateTime(1962, 12, 9),
                Reminder = false
            });

            collection.Add(new ContactDetails
            {
                ContactInfo = new Family 
                {
                    FullName = GridStrings.XG_ImplicitDataTemplates_Family1_FN,
                    NickName = GridStrings.XG_ImplicitDataTemplates_Family1_SN,
                    Relation = GridStrings.XG_ImplicitDataTemplates_FamilyRelation1
                },
                Birthday = new DateTime(2004, 5, 14),
                Reminder = true
            });

            collection.Add(new ContactDetails
            {
                ContactInfo = new Family 
                {
                    FullName = GridStrings.XG_ImplicitDataTemplates_Family2_FN,
                    NickName = GridStrings.XG_ImplicitDataTemplates_Family2_SN,
                    Relation = GridStrings.XG_ImplicitDataTemplates_FamilyRelation2
                },
                Birthday = new DateTime(2001, 9, 1),
                Reminder = true
            });

            collection.Add(new ContactDetails
            {
                ContactInfo = new Friend 
                {
                    FullName = GridStrings.XG_ImplicitDataTemplates_Friend1_FN,
                    NickName = GridStrings.XG_ImplicitDataTemplates_Friend1_SN, 
                    Email = "sss@qqq.net" 
                },
                Birthday = new DateTime(1980, 1, 30),
                Reminder = true
            });

            collection.Add(new ContactDetails
            {      
                ContactInfo = new Friend
                {
                    FullName = GridStrings.XG_ImplicitDataTemplates_Friend2_FN,
                    NickName = GridStrings.XG_ImplicitDataTemplates_Friend2_SN,
                    Email = "xxx@qqq.net"
                },
                Birthday = new DateTime(1977, 4, 1),
                Reminder = true
            });

            collection.Add(new ContactDetails
            {
                ContactInfo = new Friend
                {
                    FullName = GridStrings.XG_ImplicitDataTemplates_Friend3_FN,
                    NickName = GridStrings.XG_ImplicitDataTemplates_Friend3_SN,
                    Email = "aaaaa@qqq.net"
                },
                Birthday = new DateTime(1974, 5, 11),
                Reminder = false
            });

            return collection;
        }
    }
}
