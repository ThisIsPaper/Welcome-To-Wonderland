
namespace Wonderland.Logic.DotMailer
{
    using System.Net;
    using System.Web;
    using System.Web.Caching;
    using System.Web.Configuration;
    using Wonderland.Logic.DotMailerApi;

    internal static class DotMailerService
    {
        // address book limit = 1000
        // api calls per hour limit = 2000

        private const int PartyHostsAddressBookId = 115142;
        private const int PartyGuestsAddressBookId = 115144;

        /// <summary>
        /// add new contact (marked as a PartyHost)
        /// </summary>
        /// <param name="contact"></param>
        internal static void HostRegistrationStarted(Contact contact)
        {
            if (DotMailerService.DotMailerEnabled)
            {
                // create contact
                ApiContact apiContact = DotMailerService.GetApiService().CreateContact(contact.ToApiContact());

                // update local member with dotMailerId
                contact.Partier.DotMailerId = apiContact.Id;
            }
        }

        /// <summary>
        /// add contact to the Party Hosts address book
        /// </summary>
        /// <param name="contact"></param>
        internal static void HostRegistrationCompleted(Contact contact)
        {
            if (DotMailerService.DotMailerEnabled)
            {
                ApiService apiService = DotMailerService.GetApiService();

                // add host to address book (this also updates the contact details) - dotmailer will fire off email 2.
                apiService.AddContactToAddressBook(DotMailerService.PartyHostsAddressBookId, contact.ToApiContact());
            }
        }

        /// <summary>
        /// add new contact (marked as a Party Guest)
        /// </summary>
        /// <param name="contact"></param>
        internal static void GuestRegistrationStarted(Contact contact)
        {
            if (DotMailerService.DotMailerEnabled)
            {
                // create contact
                ApiContact apiContact = DotMailerService.GetApiService().CreateContact(contact.ToApiContact());

                // update local member with dotMailerId
                contact.Partier.DotMailerId = apiContact.Id;
            }
        }

        /// <summary>
        /// add contact to the Party Guest address book
        /// </summary>
        /// <param name="contac"></param>
        internal static void GuestRegistrationCompleted(Contact contact)
        {
            if (DotMailerService.DotMailerEnabled)
            {
                ApiService apiService = DotMailerService.GetApiService();

                // add guest to address book - dotMailer will fire off email 18
                apiService.AddContactToAddressBook(DotMailerService.PartyGuestsAddressBookId, contact.ToApiContact());
            }
        }

        internal static void UpdateContact(Contact contact)
        {
            if (DotMailerService.DotMailerEnabled)
            {
                // (hit 3: in guest signup journey, or hit 1 in donation journey)
                DotMailerService.GetApiService().UpdateContact(contact.ToApiContact());
            }
        }

        //internal static void UpdatePartyDate(PartyHost partyHost)
        //{
        //    if (DotMailerEnabled)
        //    {
        //        // TODO: need to update all guests for this party
        //    }            
        //}

        private static bool DotMailerEnabled
        {
            get
            {
                bool dotMailerEnabled;
                if (bool.TryParse(WebConfigurationManager.AppSettings["DotMailer:Enabled"], out dotMailerEnabled))
                {
                    return dotMailerEnabled;
                }

                return false;
            }
        }

        private static ApiService GetApiService()
        {
            Cache cache = HttpContext.Current.Cache;

            ApiService apiService = (ApiService)cache["ApiService"];

            if (apiService == null)
            {
                apiService = new ApiService();
                apiService.Credentials = new NetworkCredential(
                                                    WebConfigurationManager.AppSettings["DotMailer:ApiKey"],
                                                    WebConfigurationManager.AppSettings["DotMailer:Password"]);
                cache.Insert("ApiService", apiService);
            }

            return apiService;
        }       
    }
}
