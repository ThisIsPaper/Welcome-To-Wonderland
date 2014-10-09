
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

        private const int HostRegistrationStarted_AddressBookId = 114524;
        private const int HostRegistrationCompleted_AddressBookId = 114525;
        private const int GuestRegistrationStarted_AddressBookId = 114527;
        private const int GuestRegistrationCompleted_AddressBookId = 114534;

        /// <summary>
        /// add contact to the 'Host Registration Started' address book
        /// </summary>
        /// <param name="contact"></param>
        internal static void HostRegistrationStarted(Contact contact)
        {
            ApiContact apiContact = DotMailerService.GetApiService()
                                                    .AddContactToAddressBook(DotMailerService.HostRegistrationStarted_AddressBookId, contact.ToApiContact());

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

            apiService.UpdateContact(contact.ToApiContact()); // we now have firstname & lastname
            apiService.DeleteContactFromAddressBook(DotMailerService.HostRegistrationStarted_AddressBookId, contact.Id);
            apiService.AddContactToAddressBook(DotMailerService.HostRegistrationCompleted_AddressBookId, contact.ToApiContact());
        }

        /// <summary>
        /// add contact to the 'Guest Registration Started' address book
        /// </summary>
        /// <param name="contact"></param>
        internal static void GuestRegistrationStarted(Contact contact)
        {
            ApiContact apiContact = DotMailerService.GetApiService()
                                                    .AddContactToAddressBook(DotMailerService.GuestRegistrationStarted_AddressBookId, contact.ToApiContact());

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

            apiService.UpdateContact(contact.ToApiContact()); // we now have firstname & lastname
            apiService.DeleteContactFromAddressBook(DotMailerService.GuestRegistrationStarted_AddressBookId, contact.Id);
            apiService.AddContactToAddressBook(DotMailerService.GuestRegistrationCompleted_AddressBookId, contact.ToApiContact());
        }

        internal static void UpdateContact(Contact contact)
        {
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
