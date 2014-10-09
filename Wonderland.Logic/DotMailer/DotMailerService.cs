
namespace Wonderland.Logic.DotMailer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Wonderland.Logic.DotMailerApi;
    using System.Net;
    using System.Web.Configuration;
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
            ApiService apiService = DotMailerService.GetApiService();

            // create contact
            ApiContact apiContact = apiService.CreateContact(contact.ToApiContact());

            // get dotMailer id
            contact.Partier.DotMailerId = apiContact.Id;

            apiService.AddContactToAddressBook(DotMailerService.HostRegistrationStarted_AddressBookId, apiContact);
        }

        /// <summary>
        /// move contact from the 'Host Registration Started' to the 'Host Registration Completed' address book
        /// </summary>
        /// <param name="contact"></param>
        internal static void HostRegistrationCompleted(Contact contact)
        {
            ApiService apiService = DotMailerService.GetApiService();

            apiService.DeleteContactFromAddressBook(DotMailerService.HostRegistrationStarted_AddressBookId, contact.Id);
            apiService.AddContactToAddressBook(DotMailerService.HostRegistrationCompleted_AddressBookId, contact.ToApiContact());
        }

        /// <summary>
        /// add contact to the 'Guest Registration Started' address book
        /// </summary>
        /// <param name="contact"></param>
        internal static void GuestRegistrationStarted(Contact contact)
        {
            ApiService apiService = DotMailerService.GetApiService();

            // create contact
            ApiContact apiContact = apiService.CreateContact(contact.ToApiContact());

            // get dotMailer id
            contact.Partier.DotMailerId = apiContact.Id;

            apiService.AddContactToAddressBook(DotMailerService.GuestRegistrationStarted_AddressBookId, apiContact);
        }

        /// <summary>
        /// move contact from the 'Guest Registration Started' to the 'Guest Registration Completed' address book
        /// </summary>
        /// <param name="contac"></param>
        internal static void GuestRegistrationCompleted(Contact contact)
        {
        }

        //internal static void AddContact(Contact contact)
        //{
        //    ApiService apiService = DotMailerService.GetApiService();

        //    //apiService.GetAddressBooks(3, 0).Select(x => x.Name);

        //    apiService.AddContactToAddressBook(113368, contact.ToApiContact());


        //    //ApiContact gettingContactTest = apiService.GetContactById(contact.Id);
        //}

        internal static void UpdatePartyDate(PartyHost partyHost)
        {
            // TODO: need to update all guests for this party
        }

        //private static void UpdateContact(Contact contact)
        //{
        //    ApiService apiService = DotMailerService.GetApiService();

        //    apiService.UpdateContact(contact);
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
