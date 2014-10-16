
namespace Wonderland.Logic.DotMailer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Web;
    using Umbraco.Web.Security;
    using Wonderland.Logic.DotMailerApi;
    using Wonderland.Logic.Extensions;
    using Wonderland.Logic.Interfaces;
    using Wonderland.Logic.Models.Entities;
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
            this.IdSpecified = this.Id > 0;
        }

        private string FirstName
        {
            set
            {
                this.contactData.Add(new ApiContactData()
                    {
                        Key = "Firstname",
                        Value = value
                    }
                );
            }
        }

        private string LastName
        {
            set
            {
                this.contactData.Add(new ApiContactData()
                    {
                        Key = "Lastname",
                        Value = value
                    }
                );
            }
        }

        private DateTime PartyDate
        {
            set
            {
                this.contactData.Add(new ApiContactData()
                    {
                        Key = "Party_Date",
                        Value = value
                    }
                );
            }
        }

        private string PartierType
        {
            set
            {
                this.contactData.Add(new ApiContactData()
                    {
                        Key = "Partier_Type",
                        Value = value
                    }
                );
            }
        }

        private Address PartyAddress
        {
            set
            {
                this.contactData.Add(new ApiContactData() { Key = "Party_Address_1", Value = value.Address1 });
                this.contactData.Add(new ApiContactData() { Key = "Party_Address_2", Value = value.Address2 });
                this.contactData.Add(new ApiContactData() { Key = "Party_Town_City", Value = value.TownCity });
                this.contactData.Add(new ApiContactData() { Key = "Party_Postcode", Value = value.Postcode });
            }
        }

        private string PartyUrlIdentifier
        {
            set
            {
                this.contactData.Add(new ApiContactData()
                    {
                        Key = "Party_Url_Identifier",
                        Value = value
                    }
                );
            }
        }

        private int GuestCount
        {
            set
            {
                this.contactData.Add(new ApiContactData()
                    {
                        Key = "Guest_Count",
                        Value = value
                    }
                );
            }
        }

        private decimal DonationAmount
        {
            set
            {
                this.contactData.Add(new ApiContactData()
                    {
                        Key = "Donation_Amount",
                        Value = value
                    }
                );
            }
        }

        private bool PartyPageComplete
        {
            set
            {
                this.contactData.Add(new ApiContactData()
                    {
                        Key = "Party_Page_Complete",
                        Value = value
                    }
                );
            }
        }

        /// <summary>
        /// internal reference to the PartyHost or PartyGuest used to construct this contact
        /// </summary>
        internal IPartier Partier { get; set; }

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
            Contact contact = new Contact(partyHost.Email, partyHost.DotMailerId);
            contact.Partier = partyHost;

            MembershipHelper members = new MembershipHelper(UmbracoContext.Current);

            contact.FirstName = partyHost.FirstName;
            contact.LastName = partyHost.LastName;
            contact.PartyDate = partyHost.PartyDateTime;
            contact.PartierType = PartyHost.Alias;
            contact.PartyAddress = partyHost.PartyAddress;
            contact.PartyUrlIdentifier = partyHost.PartyUrlIdentifier;
            contact.GuestCount = members.GetPartiers(partyHost.PartyGuid).Count();
            contact.DonationAmount = partyHost.AmountRaised;
            contact.PartyPageComplete = partyHost.DotMailerPartyPageComplete;

            return contact;
        }

        public static explicit operator Contact(PartyGuest partyGuest)
        {
            Contact contact = new Contact(partyGuest.Email, partyGuest.DotMailerId);
            contact.Partier = partyGuest;

            PartyHost partyHost = new MembershipHelper(UmbracoContext.Current).GetPartyHost(partyGuest.PartyGuid);

            contact.FirstName = partyGuest.FirstName;
            contact.LastName = partyGuest.LastName;
            contact.PartyDate = partyHost.PartyDateTime;
            contact.PartierType = PartyGuest.Alias;
            contact.PartyAddress = partyHost.PartyAddress;
            contact.PartyUrlIdentifier = partyHost.PartyUrlIdentifier;

            return contact;
        }
    }
}
