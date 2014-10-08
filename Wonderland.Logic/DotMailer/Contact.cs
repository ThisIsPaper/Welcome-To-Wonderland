
namespace Wonderland.Logic.DotMailer
{
    using System;
    using System.Collections.Generic;
    using Umbraco.Web;
    using Umbraco.Web.Security;
    using Wonderland.Logic.DotMailerApi;
    using Wonderland.Logic.Extensions;
    using Wonderland.Logic.Models.Members;

    public class Contact : ApiContact
    {
        private List<ApiContactData> contactData = new List<ApiContactData>();

        private Contact(string email, int id)
        {
            this.Email = email;
            this.EmailType = ApiContactEmailTypes.Html;
            this.EmailTypeSpecified = true; // if this flag isn't set, then the EmailType isn't processed
            this.Id = id;
            this.IdSpecified = true;
        }

        private DateTime PartyDate
        {
            set
            {
                this.contactData.Add(new ApiContactData()
                    {
                        Key="Party_Date",
                        Value=value,
                    }
                );
            }
        }

        /// <summary>
        /// need to explicity create a new ApiContact obj rather than via a cast, as the built-in serialization uses reflection to see what the full type is
        /// </summary>
        /// <returns></returns>
        internal ApiContact ToApiContact()
        {
            return new ApiContact()
                {
                    Email = this.Email,
                    EmailType = this.EmailType,
                    EmailTypeSpecified = this.EmailTypeSpecified,
                    Id = this.Id,
                    IdSpecified = this.IdSpecified,
                    DataFields = this.contactData.ToArray()
                };            
        }

        public static explicit operator Contact(PartyHost partyHost)
        {
            Contact contact = new Contact(partyHost.Email, partyHost.Id);

            contact.PartyDate = partyHost.PartyDateTime;

            return contact;
        }

        public static explicit operator Contact(PartyGuest partyGuest)
        {
            Contact contact = new Contact(partyGuest.Email, partyGuest.Id);

            PartyHost partyHost = new MembershipHelper(UmbracoContext.Current).GetPartyHost(partyGuest.PartyGuid);

            contact.PartyDate = partyHost.PartyDateTime;
            
            return contact;
        }
    }
}
