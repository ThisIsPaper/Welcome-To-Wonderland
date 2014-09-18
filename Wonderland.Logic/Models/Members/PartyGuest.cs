
namespace Wonderland.Logic.Models.Members
{
    using System;
    using System.Linq;
    using System.Web.Security;
    using Umbraco.Core.Models;
    using Umbraco.Web;
    using Umbraco.Web.Security;
    using Wonderland.Logic.Interfaces;
    using Wonderland.Logic.Models.Content;
    using Wonderland.Logic.Models.Entities;

    public class PartyGuest : BaseMemberType, IPartier
    {
        // Member Type
        public const string Alias = "PartyGuest";

        // Properties
        public const string PartyGuidAlias = "partyGuid";
        public const string FirstNameAlias = "firstName";
        public const string LastNameAlias = "lastName";
        public const string BillingAddressAlias = "billingAddress";
        public const string ProfileImageAlias = "profileImage";

        public PartyGuest(IPublishedContent content)
            : base(content)
        {
        }

        /// <summary>
        /// once created, never changes - this guid identifies a specific party
        /// </summary>
        public Guid PartyGuid
        {
            get
            {
                return this.GetPropertyValue<Guid>(PartyHost.PartyGuidAlias);
            }
            set
            {
                this.SetPropertyValue(PartyHost.PartyGuidAlias, value.ToString());
            }
        }

        public string FirstName
        {
            get
            {
                return this.GetPropertyValue<string>(PartyHost.FirstNameAlias);
            }
            set
            {
                this.SetPropertyValue(PartyHost.FirstNameAlias, value);
            }
        }

        public string LastName
        {
            get
            {
                return this.GetPropertyValue<string>(PartyHost.LastNameAlias);
            }
            set
            {
                this.SetPropertyValue(PartyHost.LastNameAlias, value);
            }
        }

        public Address BillingAddress
        {
            get
            {
                return new Address(this.GetPropertyValue<string>(PartyGuest.BillingAddressAlias));
            }
            set
            {
                this.SetPropertyValue(PartyGuest.BillingAddressAlias, value.ToString());
            }
        }

        public string ProfileImage
        {
            get
            {
                return this.GetPropertyValue<string>(PartyGuest.ProfileImageAlias);
            }
            set
            {
                this.SetPropertyValue(PartyGuest.ProfileImageAlias, value);
            }
        }

        public string PersonName
        {
            get
            {
                // TODO:
                return "unknown guest";
            }
        }

        public string ProfileImageUrl
        {
            get
            {
                return "/Uploads/Profile/" + this.ProfileImage;
            }
        }

        /// <summary>
        /// using the PartyGuid, finds the host to get the PartyUrlIdentification property
        /// </summary>
        /// <returns></returns>
        public string PartyUrl
        {
            get
            {
                //PartyHost partyhost = PartyHost.GetByPartyGuid(this.PartyGuid);

                // WARNING: Hits DB
                IMember partyHost = this.MemberService
                                        .GetMembersByMemberType(PartyHost.Alias)
                                        .Single(x => x.GetValue<Guid>(PartyHost.PartyGuidAlias) == this.PartyGuid);

                // Jeavons suggestion to get published members:
                // this.Members.GetCurrentMember().Parent.Children; (this is null)
            
                return UmbracoContext.Current.ContentCache.GetSingleByXPath("//" + Party.Alias).Url + partyHost.GetValue<string>(PartyHost.PartyUrlIdentifierAlias);
            }
        }

        public static explicit operator PartyGuest(MembershipUser membershipUser)
        {
            return new PartyGuest(new MembershipHelper(UmbracoContext.Current).GetByUsername(membershipUser.UserName));
        }
    }
}
