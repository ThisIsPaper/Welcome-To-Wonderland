
namespace Wonderland.Web.Dashboards
{
    using System;
    using System.Linq;
    using System.Text;
    using Wonderland.Logic.Extensions;
    using Wonderland.Logic.Interfaces;
    using Wonderland.Logic.Models.Members;

    public partial class ExportRegistrations : BaseUserControl
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.exportRegistrationsButton.Click += this.ExportRegistrationsButton_Click;
        }

        private void ExportRegistrationsButton_Click(object sender, EventArgs e)
        {
            StringBuilder stringBuilder = new StringBuilder();

            // headings
            stringBuilder.AppendLine("Member Id, Member Type, Email, First Name, Last Name, Heard About Us, Registration Date, Address 1, Address 2, Town, Postcode, T-Shirt Size, Party Guid, Party Title, Party DateTime, Party Address 1, Party Address 2, Party Town, Party Postcode, Join Wonderland Story, Party Background Image, Profile Photo, Fundraising Target, Suggested Donation, Guest Count, Amount Raised, Party Blocked, DotMailer Id, DotMailer Registration Complete, DotMailer Party Page Copmlete");          

            foreach(IPartier partier in this.Members.GetAllPartiers())
            {
                if (partier is PartyHost)
                {
                    PartyHost partyHost = (PartyHost)partier;

                    stringBuilder.AppendLine(
                        string.Join(
                            ", ",
                            new object[] { 
                                partier.Id,                                                                                     // 1.  Member Id
                                PartyHost.Alias,                                                                                // 2.  Member Type
                                partier.Email.Replace(',', ' '),                                                                // 3.  Email
                                partier.FirstName.Replace(',', ' '),                                                            // 4.  First Name
                                partier.LastName.Replace(',', ' '),                                                             // 5.  Last Name
                                partyHost.MarketingSource != null ? partyHost.MarketingSource.Replace(',', ' ') : string.Empty, // 6.  Heard About Us
                                partyHost.CreateDate,                                                                           // 7.  Registration Date
                                partier.BillingAddress.Address1.Replace(',', ' '),                                              // 8.  Billing Address 1
                                partier.BillingAddress.Address2.Replace(',', ' '),                                              // 9.  Billing Address 2
                                partier.BillingAddress.TownCity.Replace(',', ' '),                                              // 10. Billing TownCity
                                partier.BillingAddress.Postcode.Replace(',', ' '),                                              // 11. Billing Postcode
                                partyHost.TShirtSize,                                                                           // 12. T-Shirt Size
                                partier.PartyGuid,                                                                              // 13. Party Guid
                                partyHost.PartyHeading != null ? partyHost.PartyHeading.Replace(',', ' ') : string.Empty,       // 14. Party Title
                                partyHost.PartyDateTime,                                                                        // 15. Party DateTime 
                                partyHost.PartyAddress.Address1.Replace(',', ' '),                                              // 16. Party Address 1
                                partyHost.PartyAddress.Address2.Replace(',', ' '),                                              // 17. Party Address 2
                                partyHost.PartyAddress.TownCity.Replace(',', ' '),                                              // 18. Party TownCity
                                partyHost.PartyAddress.Postcode.Replace(',', ' '),                                              // 19. Party Postcode
                                partyHost.PartyCopy != null ? partyHost.PartyCopy.Replace(',', ' ') : string.Empty,             // 20. Join Wonderland Story
                                partyHost.PartyImage != null ? "Y" : "N",                                                       // 21. Party Background Image
                                !string.IsNullOrWhiteSpace(partier.ProfileImage) ? "Y" : "N",                                   // 22. Profile Photo
                                partyHost.FundraisingTarget,                                                                    // 23. Fundraising Target
                                partyHost.SuggestedDonation,                                                                    // 24. Suggested Donation
                                this.Members.GetPartiers(partyHost.PartyGuid).Count(),                                          // 25. Guest Count
                                partyHost.AmountRaised,                                                                         // 26. Amount Raised
                                partyHost.Blocked ? "Y" : "N",                                                                  // 27. Party Blocked
                                partier.DotMailerId,                                                                            // 28. DotMailer Id
                                partier.DotMailerRegistrationComplete ? "Y" : "N",                                              // 29. DotMailer Registration Complete
                                partyHost.DotMailerPartyPageComplete ? "Y" : "N"                                                // 30. DotMailer Party Page Complete
                            }
                        )
                    );
                }
                else // must be a guest
                {
                    PartyGuest partyGuest = (PartyGuest)partier;

                    stringBuilder.AppendLine(
                        string.Join(
                            ", ",
                            new object[] { 
                                partier.Id,                                                                             // 1.  Member Id
                                PartyGuest.Alias,                                                                       // 2.  Member Type
                                partier.Email.Replace(',', ' '),                                                        // 3.  Email
                                partier.FirstName.Replace(',', ' '),                                                    // 4.  First Name
                                partier.LastName.Replace(',', ' '),                                                     // 5.  Last Name
                                string.Empty,                                                                           // 6.  Heard About Us
                                partyGuest.CreateDate,                                                                  // 7.  Registration Date
                                partier.BillingAddress.Address1.Replace(',', ' '),                                      // 8.  Billing Address 1
                                partier.BillingAddress.Address2.Replace(',', ' '),                                      // 9.  Billing Address 2
                                partier.BillingAddress.TownCity.Replace(',', ' '),                                      // 10. Billing TownCity
                                partier.BillingAddress.Postcode.Replace(',', ' '),                                      // 11. Billing Postcode
                                string.Empty,                                                                           // 12. T-Shirt Size
                                partier.PartyGuid,                                                                      // 13. Party Guid
                                string.Empty,                                                                           // 14. Party Title
                                string.Empty,                                                                           // 15. Party DateTime 
                                string.Empty,                                                                           // 16. Party Address 1
                                string.Empty,                                                                           // 17. Party Address 2
                                string.Empty,                                                                           // 18. Party TownCity
                                string.Empty,                                                                           // 19. Party Postcode
                                string.Empty,                                                                           // 20. Join Wonderland Story
                                string.Empty,                                                                           // 21. Party Background Image
                                string.IsNullOrWhiteSpace(partier.ProfileImage) ? "Y" : "N",                            // 22. Profile Photo
                                string.Empty,                                                                           // 23. Fundraising Target
                                string.Empty,                                                                           // 24. Suggested Donation
                                string.Empty,                                                                           // 25. Guest Count
                                string.Empty,                                                                           // 26. Amount Raised
                                string.Empty,                                                                           // 27. Party Blocked
                                partier.DotMailerId,                                                                    // 28. DotMailer Id
                                partier.DotMailerRegistrationComplete ? "Y" : "N",                                      // 29. DotMailer Registration Complete
                                string.Empty                                                                            // 30. DotMailer Party Page Complete
                            }
                        )
                    );
                }
            }

            // steream out the response
            this.Response.Clear();
            this.Response.AddHeader("content-disposition", "attachment; filename=registrations.csv");
            this.Response.ContentType = "text/csv";
            this.Response.ContentEncoding = Encoding.UTF8;
            this.Response.Write(stringBuilder.ToString());
            this.Response.End();
        }
    }
}