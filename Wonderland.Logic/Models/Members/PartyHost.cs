
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
    using Wonderland.Logic.Models.Media;
    using Umbraco.Core.Services;

    public class PartyHost : Partier, IPartier
    {
        public static DateTime DefaultPartyDate = new DateTime(2014, 12, 5, 20, 0, 0);

        // Member Type
        public const string Alias = "PartyHost";

        // Properties
        public const string PartyGuidAlias = "partyGuid";
        public const string FirstNameAlias = "firstName";
        public const string LastNameAlias = "lastName";
        public const string BillingAddressAlias = "billingAddress";
        //public const string ProfileImageAlias = "profileImage";
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
        public const string FacebookRegistrationAlias = "facebookRegistration";

        // local cache variables (avoids waiting for round trip to update umbraco cache values)
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

        public IPartyImage PartyImage
        {
            get
            {
                int? imageId = (int?)this.GetPropertyValue(PartyHost.PartyImageAlias);

                if (imageId.HasValue && imageId > 0)
                {
                    return (IPartyImage)this.Umbraco.TypedMedia(imageId.Value);
                }

                return null;
            }
            set 
            {
                if (value == null)
                {
                    // if there is a custom uploaded party image (rather than a chosen default)
                    if (this.PartyImage is PartyImage)
                    {
                        // delete image                   
                        IMedia media = this.MediaService.GetById(this.PartyImage.Id);
                        this.MediaService.Delete(media);
                    }

                    // remove reference
                    this.SetPropertyValue(PartyHost.PartyImageAlias, null);
                }
                else
                {
                    // if changing
                    if (this.PartyImage == null || this.PartyImage.Id != value.Id)
                    {
                        // update reference
                        this.SetPropertyValue(PartyHost.PartyImageAlias, value.Id);
                    }
                }
            }
        }

        /// <summary>
        /// Gets the party heading for the current party host
        /// </summary>
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

        /// <summary>
        /// Gets the party copy for the current party host, or if not set, then returns the default party copy
        /// </summary>
        public string PartyCopy
        {
            get
            {                
                string partyCopy = this.GetPropertyValue<string>(PartyHost.PartyCopyAlias);

                if(string.IsNullOrWhiteSpace(partyCopy))
                {
                    partyCopy = ((Party)new UmbracoHelper(UmbracoContext.Current).TypedContentSingleAtXPath("//" + Party.Alias)).DefaultCopy;
                }

                return partyCopy;
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

        public bool FacebookRegistration
        {
            get
            {
                return this.GetPropertyValue<bool>(PartyHost.FacebookRegistrationAlias);
            }
            set
            {
                this.SetPropertyValue(PartyHost.FacebookRegistrationAlias, value);
            }
        }

        // left in so as to avoid lots of view changes - ideally this would be removed
        public string ProfileImageUrl
        {
            get
            {
                if (this.ProfileImage != null)
                {
                    return this.ProfileImage.Url; //TODO:S3URL
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
