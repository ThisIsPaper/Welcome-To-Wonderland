
namespace Wonderland.Logic.Models.Members
{
    using System;
    using System.Linq;
    using System.Web.Security;
    using Umbraco.Core;
    using Umbraco.Core.Models;
    using Umbraco.Web;
    using Umbraco.Web.Security;
    using Wonderland.Logic.Interfaces;
    using Wonderland.Logic.Models.Content;
    using Wonderland.Logic.Models.Entities;
    using Wonderland.Logic.Extensions;

    public class PartyHost : BaseMemberType, IPartier
    {
        // Member Type
        public const string Alias = "PartyHost";

        // Properties
        public const string PartyGuidAlias = "partyGuid";
        public const string FirstNameAlias = "firstName";
        public const string LastNameAlias = "lastName";
        public const string BillingAddressAlias = "billingAddress";
        public const string ProfileImageAlias = "profileImage";
        public const string BlockedAlias = "blocked";
        public const string MarketingSourceAlias = "marketingSource";
        public const string PartyKitAddressAlias = "partyKitAddress";
        public const string PartyAddressAlias = "partyAddress";
        public const string PartyDateTimeAlias = "partyDateTime";
        public const string TShirtSizeAlias = "tShirtSize";
        public const string HasRequestedPartyKitAlias = "hasRequestedPartyKit";        
        public const string PartyUrlIdentifierAlias = "partyUrlIdentifier";
        public const string PartyImageAlias = "partyImage";
        public const string PartyHeadingAlias = "partyHeading";
        public const string PartyCopyAlias = "partyCopy";
        public const string SuggestedDonationAlias = "suggestedDonation";
        public const string FundraisingTargetAlias = "fundraisingTarget";
        public const string ForgottenPasswordGuidAlias = "forgottenPasswordGuid";
        public const string DotMailerIdAlias = "dotMailerId";
        public const string DotMailerRegistrationCompleteAlias = "dotMailerRegistrationComplete";
        public const string DotMailerPartyPageCompleteAlias = "dotMailerPartyPageComplete";

        private Guid? partyGuid = null;
        private string firstName = null;
        private string lastName = null;
        private Address billingAddress = null;
        private Address partyAddress = null;
        private DateTime? partyDateTime = null;
        private string partyUrlIdentifier = null;
        private bool? dotMailerPartyPageComplete = null;
        private decimal? amountRaised = null;
        private int? totalGuests = null;

        public PartyHost(IPublishedContent content)
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
                if (this.partyGuid.HasValue)
                {
                    return this.partyGuid.Value;
                }

                return this.GetPropertyValue<Guid>(PartyHost.PartyGuidAlias);
            }
            set
            {
                this.partyGuid = value;
                this.SetPropertyValue(PartyHost.PartyGuidAlias, value.ToString());
            }
        }

        public string FirstName
        {
            get
            {
                if (this.firstName != null)
                {
                    return this.firstName;
                }

                return this.GetPropertyValue<string>(PartyHost.FirstNameAlias);
            }
            set
            {
                this.firstName = value;
                this.SetPropertyValue(PartyHost.FirstNameAlias, value);
            }
        }

        public string LastName
        {
            get
            {
                if (this.lastName != null)
                {
                    return this.lastName;
                }

                return this.GetPropertyValue<string>(PartyHost.LastNameAlias);
            }
            set
            {
                this.lastName = value;
                this.SetPropertyValue(PartyHost.LastNameAlias, value);
            }
        }

        public Address BillingAddress
        {
            get
            {
                if (this.billingAddress != null)
                {
                    return this.billingAddress;
                }

                return new Address(this.GetPropertyValue<string>(PartyHost.BillingAddressAlias));
            }
            set
            {
                this.billingAddress = value;
                this.SetPropertyValue(PartyHost.BillingAddressAlias, value.ToString());
            }
        }

        public string ProfileImage
        {
            get
            {
                return this.GetPropertyValue<string>(PartyHost.ProfileImageAlias);
            }
            set
            {
                this.SetPropertyValue(PartyHost.ProfileImageAlias, value);
            }
        }

        public bool Blocked
        {
            get
            {
                return this.GetPropertyValue<bool>(PartyHost.BlockedAlias);
            }
        }

        public string MarketingSource
        {
            get
            {
                return this.GetPropertyValue<string>(PartyHost.MarketingSourceAlias);
            }
            set
            {
                this.SetPropertyValue(PartyHost.MarketingSourceAlias, value);
            }
        }

        public Address PartyKitAddress
        {
            get
            {
                return new Address(this.GetPropertyValue<string>(PartyHost.PartyKitAddressAlias));
            }
            set 
            {
                this.SetPropertyValue(PartyHost.PartyKitAddressAlias, value.ToString());
            }
        }

        public Address PartyAddress
        {
            get
            {
                if (this.partyAddress != null)
                {
                    return this.partyAddress;
                }

                return new Address(this.GetPropertyValue<string>(PartyHost.PartyAddressAlias));
            }
            set
            {
                this.partyAddress = value;
                this.SetPropertyValue(PartyHost.PartyAddressAlias, value.ToString());
            }
        }

        public DateTime PartyDateTime
        {
            get
            {
                if (this.partyDateTime.HasValue)
                {
                    return this.partyDateTime.Value;
                }

                return this.GetPropertyValue<DateTime>(PartyHost.PartyDateTimeAlias);
            }
            set
            {
                this.partyDateTime = value;
                this.SetPropertyValue(PartyHost.PartyDateTimeAlias, value.ToString());
            }
        }

        public string TShirtSize
        {
            get
            {
                return this.GetPropertyValue<string>(PartyHost.TShirtSizeAlias);
            }
            set
            {
                this.SetPropertyValue(PartyHost.TShirtSizeAlias, value);
            }
        }

        public bool HasRequestedPartyKit
        {
            get
            {
                return this.GetPropertyValue<bool>(PartyHost.HasRequestedPartyKitAlias);
            }
            set
            {
                this.SetPropertyValue(PartyHost.HasRequestedPartyKitAlias, value);
            }
        }

        /// <summary>
        /// defaults to the party guid, but TODO: feature to allow for a custom url for a party
        /// </summary>
        public string PartyUrlIdentifier
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(this.partyUrlIdentifier))
                {
                    return this.partyUrlIdentifier;
                }

                return this.GetPropertyValue<string>(PartyHost.PartyUrlIdentifierAlias);
            }
            set
            {
                this.partyUrlIdentifier = value;
                this.SetPropertyValue(PartyHost.PartyUrlIdentifierAlias, value);
            }
        }

        public string PartyImage
        {
            get
            {
                return this.GetPropertyValue<string>(PartyHost.PartyImageAlias);
            }
            set
            {
                this.SetPropertyValue(PartyHost.PartyImageAlias, value);
            }
        }

        public string PartyImageUrl
        {
            get
            {
                return "/Uploads/PartyImage/" + this.PartyImage;
            }
        }

        public string PartyHeading
        {
            get
            {
                return this.GetPropertyValue<string>(PartyHost.PartyHeadingAlias);
            }
            set
            {
                this.SetPropertyValue(PartyHost.PartyHeadingAlias, value);
            }
        }

        public string PartyCopy
        {
            get
            {
                return this.GetPropertyValue<string>(PartyHost.PartyCopyAlias);
            }
            set
            {
                this.SetPropertyValue(PartyHost.PartyCopyAlias, value);
            }
        }

        public decimal SuggestedDonation
        {
            get
            {
                return this.GetPropertyValue<decimal>(PartyHost.SuggestedDonationAlias);
            }
            set
            {
                this.SetPropertyValue(PartyHost.SuggestedDonationAlias, value.ToString());
            }
        }

        public decimal FundraisingTarget
        {
            get
            {
                return this.GetPropertyValue<decimal>(PartyHost.FundraisingTargetAlias);
            }
            set
            {
                this.SetPropertyValue(PartyHost.FundraisingTargetAlias, value.ToString());
            }
        }

        public Guid ForgottenPasswordGuid
        {
            get
            {
                return this.GetPropertyValue<Guid>(PartyHost.ForgottenPasswordGuidAlias);
            }
            set
            {
                this.SetPropertyValue(PartyHost.ForgottenPasswordGuidAlias, value.ToString("D").ToLower());
            }
        }

        public int DotMailerId
        {
            get
            {
                return this.GetPropertyValue<int>(PartyHost.DotMailerIdAlias);
            }
            set
            {
                this.SetPropertyValue(PartyHost.DotMailerIdAlias, value);
            }
        }

        public bool DotMailerRegistrationComplete
        {
            get
            {
                return this.GetPropertyValue<bool>(PartyHost.DotMailerRegistrationCompleteAlias);
            }
            set
            {
                this.SetPropertyValue(PartyHost.DotMailerRegistrationCompleteAlias, value);
            }
        }
        
        public bool DotMailerPartyPageComplete
        {
            get
            {
                if (this.dotMailerPartyPageComplete.HasValue)
                {
                    return this.dotMailerPartyPageComplete.Value;
                }

                return this.GetPropertyValue<bool>(PartyHost.DotMailerPartyPageCompleteAlias);
            }
            set
            {
                this.dotMailerPartyPageComplete = value;
                this.SetPropertyValue(PartyHost.DotMailerPartyPageCompleteAlias, value);
            }
        }

        public string ProfileImageUrl
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(this.ProfileImage))
                {
                    return "/Uploads/Profile/" + this.ProfileImage;
                }

                return null;                
            }          
        }

        public string PartyUrl
        {
            get
            {
                return UmbracoContext.Current.ContentCache.GetSingleByXPath("//" + Party.Alias).Url + this.PartyUrlIdentifier;
            }
        }

        public decimal AmountRaised
        {
            get
            {
                if (!this.amountRaised.HasValue)
                {
                    try
                    {
                        this.amountRaised = this.DatabaseContext.Database.ExecuteScalar<decimal>(@"
                                                                                                    SELECT  SUM(Amount)
                                                                                                    FROM    wonderlandDonation
                                                                                                    WHERE   PartyGuid = @0
                                                                                                            AND Success = 1
                                                                                                ", this.PartyGuid);
                    }
                    catch
                    {
                        this.amountRaised = 0;
                    }
                }

                return this.amountRaised.Value;
            }
            internal set
            {
                this.amountRaised = value;
            }
        }
        
        public int TotalGuests
        {
            get
            {                
                if (!this.totalGuests.HasValue)
                {
                    // no value set, so calculate
                    this.totalGuests = this.Members.GetPartiers(this.PartyGuid).Count();
                }

                return this.totalGuests.Value;
            }
            internal set
            {
                // used to preset total guests for a number of party hosts in a single loop (results of a single sql query)
                this.totalGuests = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="membershipUser"></param>
        /// <returns></returns>
        public static explicit operator PartyHost(MembershipUser membershipUser)
        {
            return new PartyHost(new MembershipHelper(UmbracoContext.Current).GetByUsername(membershipUser.UserName));
        }
    }
}
