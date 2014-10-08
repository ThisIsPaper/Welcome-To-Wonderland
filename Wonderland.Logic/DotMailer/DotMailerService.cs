
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

        private const int PartyHostsAddressBookId = 105328;
        private const int PartyGuestsAddressBookId = 113368;


        internal static void StartedHostRegistration(Contact contact)
        {
        }

        internal static void StartedGuestRegistration(Contact contact)
        {
        }

        internal static void CompletedHostRegistration(Contact contact)
        {
        }

        internal static void CompletedGuestRegistration(Contact contac)
        {
        }

        internal static void AddContact(Contact contact)
        {
            ApiService apiService = DotMailerService.GetApiService();

            //apiService.GetAddressBooks(3, 0).Select(x => x.Name);

            apiService.AddContactToAddressBook(113368, contact.ToApiContact());


            //ApiContact gettingContactTest = apiService.GetContactById(contact.Id);
        }

        internal static void UpdatePartyDate(PartyHost partyHost)
        {
            // TODO: need to update all guests for this party
        }


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
