
namespace Wonderland.Logic.DotMailer
{
    using System.Net;
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
            // create contact
            ApiContact apiContact = DotMailerService.GetApiService().CreateContact(contact.ToApiContact());

            // update local member with dotMailerId
            contact.Partier.DotMailerId = apiContact.Id;
        }

        /// <summary>
        /// add contact to the Party Hosts address book
        /// </summary>
        /// <param name="contact"></param>
        internal static void HostRegistrationCompleted(Contact contact)
        {
            ApiService apiService = DotMailerService.GetApiService();

            // add host to address book (this also updates the contact details) - dotmailer will fire off email 2.
            apiService.AddContactToAddressBook(DotMailerService.PartyHostsAddressBookId, contact.ToApiContact());
        }

        /// <summary>
        /// add new contact (marked as a Party Guest)
        /// </summary>
        /// <param name="contact"></param>
        internal static void GuestRegistrationStarted(Contact contact)
        {
            // create contact
            ApiContact apiContact = DotMailerService.GetApiService()
                                                    .CreateContact(contact.ToApiContact());

            // update local member with dotMailerId
            contact.Partier.DotMailerId = apiContact.Id;
        }

        /// <summary>
        /// add contact to the Party Guest address book
        /// </summary>
        /// <param name="contac"></param>
        internal static void GuestRegistrationCompleted(Contact contact)
        {
            ApiService apiService = DotMailerService.GetApiService();

            // add guest to address book - dotMailer will fire off email 18
            apiService.AddContactToAddressBook(DotMailerService.PartyGuestsAddressBookId, contact.ToApiContact());            
        }

        internal static void UpdateContact(Contact contact)
        {
            // (hit 4: in guest signup journey)
            DotMailerService.GetApiService().UpdateContact(contact.ToApiContact());
        }

        //internal static void UpdatePartyDate(PartyHost partyHost)
        //{
        //    // TODO: need to update all guests for this party
        //}

        private static ApiService GetApiService()
        {
            ApiService apiService = new ApiService();

            apiService.Credentials = new NetworkCredential(
                                                WebConfigurationManager.AppSettings["DotMailer:ApiKey"],
                                                WebConfigurationManager.AppSettings["DotMailer:Password"]);

            return apiService;
        }
    }
}
