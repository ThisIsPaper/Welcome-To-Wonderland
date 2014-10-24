﻿
namespace Wonderland.Logic.Models.Members
{
    using System;
    using System.Linq;
    using System.Web.Security;
    using Umbraco.Core.Models;
    using Umbraco.Web;
    using Umbraco.Web.Security;
    using Wonderland.Logic.Extensions;
    using Wonderland.Logic.Interfaces;
    using Wonderland.Logic.Models.Content;
    using Wonderland.Logic.Models.Entities;
    using Wonderland.Logic.Models.Media;

    public class PartyHost : Partier, IPartier
    {
        public static DateTime DefaultPartyDate = new DateTime(2014, 12, 5, 20, 0, 0);

        // Member Type
        public const string Alias = "PartyHost";

        // Properties
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
        public const string DotMailerPartyPageCompleteAlias = "dotMailerPartyPageComplete";

        // Local Cache
        private Address partyAddress = null;
        private DateTime? partyDateTime = null;
        private string partyUrlIdentifier = null;
        private bool? dotMailerPartyPageComplete = null;
        private decimal? amountRaised = null;
        private int? totalGuests = null;
        private IPartyImage partyImage = null;

        public PartyHost(IPublishedContent content)
            : base(content)
        {
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
                if (this.partyImage != null)
                {
                    return this.partyImage;
                }

                int? imageId = (int?)this.GetPropertyValue(PartyHost.PartyImageAlias);

                if (imageId.HasValue && imageId > 0)
                {
                    return (IPartyImage)this.Umbraco.TypedMedia(imageId.Value);
                }

                return null;
            }
            set 
            {
                this.partyImage = value;

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
                    // update reference
                    this.SetPropertyValue(PartyHost.PartyImageAlias, value.Id);
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
