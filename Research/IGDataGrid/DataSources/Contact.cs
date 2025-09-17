using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infragistics.Samples.Shared.DataProviders;

namespace IGDataGrid.Datasources
{
    /// <summary>
    /// This is the data source for the XamDataGrid's 'Adorning Editors' sample.
    /// </summary>
    public class Contact
    {
        public static Contact[] CreateContacts()
        {
            return new Contact[]
            {   /*          OnlineStatus  FirstName   LastName      NickName   ImageFile       CellPhone         HomePhone         WorkPhone              PersonalEmail           WorkEmail                     Notes   */
                new Contact("Online",     "Joe",      "Barkley",    "Joey",    "GUY01.JPG",    "(718) 555 9283", "(718) 555 0012", "(631) 555 9312 x31",  "joey@hotmail.com",     "joe.barkley@acme.com",      "I owe Joe tickets to the next Giants game.  His wife Joan sells motorcycles and might be able to get me a good deal."),
                new Contact("Offline",    "Michelle", "Santiago",   "Maggie",  "GIRL03.JPG",   "(203) 555 7231", "(203) 555 1321", "(203) 555 1925 x277", "michelle@gmail.com",   "msantiago@notacompany.com", "Met her at the conference in Panama City back in 2006.  She moved to Los Angeles two years ago, to work in the film industry."),
                new Contact("Offline",    "Patricia", "McClellan",  "Trish",   "GIRL01.JPG",   "(718) 555 6712", "(718) 555 0190", "(718) 555 4414 x22",  "trish@hotmail.com",    "pmcclellan@foobarCo.com",   "We met in Las Vegas and discussed a joint venture involving real estate speculation.  She lives in Denver and works in sales."),
                new Contact("Online",     "David",    "Esterhazy",  "Dave",    "GUY02.JPG",    "(617) 555 2212", "(617) 555 8120", "(617) 555 8811 x5",   "david@yahoo.com",      "esterhazy@foobarCo.com",    "Dave introduced me to Maggie at the conference in Panama.  He has a lot of contacts in the German banking world."),
                new Contact("Online",     "Steven",   "Rottingham", "Steve",   "GUY03.JPG",    "(201) 555 3449", "(201) 555 9824", "(201) 555 7719 x843", "steve@hotmail.com",    "stever@acmetech.com",       "We worked together on the ProjX system back at AcmeTech in 2004.  He is a big Yankees fan, and is interested in antiques.")
            };
        }

        private Contact(
            string onlineStatus,
            string firstName, string lastName, string nickName,
            string imageFile,
            string cellPhone, string homePhone, string workPhone,
            string personalEmail, string workEmail,
            string notes)
        {
            this.OnlineStatus = onlineStatus;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.NickName = nickName;
            this.ImageUri = ImageFilePathProvider.GetImageLocalUri("People/" + imageFile);
            this.CellPhone = cellPhone;
            this.HomePhone = homePhone;
            this.WorkPhone = workPhone;
            this.PersonalEmail = personalEmail;
            this.WorkEmail = workEmail;
            this.Notes = notes;
        }

        public string OnlineStatus { get; private set; }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string NickName { get; private set; }

        public string DisplayName
        {
            get { return String.Format("{0}, {1}", this.LastName, this.FirstName); }
        }

        public Uri ImageUri { get; private set; }

        public string CellPhone { get; private set; }
        public string HomePhone { get; private set; }
        public string WorkPhone { get; private set; }

        public string PersonalEmail { get; private set; }
        public string WorkEmail { get; private set; }

        public string Notes { get; set; }
    }
}
