
namespace Wonderland.Logic.DotMailer
{
    using System.Net;
    using System.Web.Configuration;
    using Wonderland.Logic.DotMailerApi;
    using Wonderland.Logic.Models.Members;

    internal static class DotMailerService
    {
        // address book limit = 1000
        // api calls per hour limit = 2000

        private const int PartyHostsAddressBookId = 115142;
        private const int PartyGuestsAddressBookId = 115144;

        /// <summary>
        /// add contact to the 'Host Registration Started' address book
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
        /// move contact from the 'Host Registration Started' to the 'Host Registration Completed' address book
        /// </summary>
        /// <param name="contact"></param>
        internal static void HostRegistrationCompleted(Contact contact)
        {
            ApiService apiService = DotMailerService.GetApiService();

            // update Contact not needed, as contact is updated when adding to the address book
            //apiService.UpdateContact(contact.ToApiContact()); // we now have firstname & lastname

            // add host to address book - dotmailer will fire off email 2.
            apiService.AddContactToAddressBook(DotMailerService.PartyHostsAddressBookId, contact.ToApiContact());
        }

        /// <summary>
        /// add contact to the 'Guest Registration Started' address book
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
        /// move contact from the 'Guest Registration Started' to the 'Guest Registration Completed' address book
        /// </summary>
        /// <param name="contac"></param>
        internal static void GuestRegistrationCompleted(Contact contact)
        {
            ApiService apiService = DotMailerService.GetApiService();

            //// hit 2: update guest contact - can we avoid this ?
            //apiService.UpdateContact(contact.ToApiContact()); // we now have firstname & lastname

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
