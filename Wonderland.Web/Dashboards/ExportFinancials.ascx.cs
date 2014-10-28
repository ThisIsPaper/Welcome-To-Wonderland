
namespace Wonderland.Web.Dashboards
{
    using System;
    using System.Text;
    using Umbraco.Core;
    using Umbraco.Web;
    using Umbraco.Web.Security;
    using Wonderland.Logic.Models.Database;
    using Wonderland.Logic.Extensions;
    using Wonderland.Logic.Interfaces;
    using Wonderland.Logic.Models.Members;
    using Umbraco.Core.Logging;

    public partial class ExportFinancials : BaseUserControl
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.exportFinancialsButton.Click += this.ExportFinancialsButton_Click;
        }

        private void ExportFinancialsButton_Click(object sender, EventArgs e)
        {
            StringBuilder stringBuilder = new StringBuilder();

            // headings            
            stringBuilder.AppendLine(@"VpsTxId, VendorTxCode, Success, Member Id, Donor Email, Donor First Name, Donor Last Name, Donor Billing Address 1, Donor Billing Address 2, Donor Billing Town City, Donor Billing Postcode, Amount Donated, Date, Time, Gift Aid, Payment Journey, Opt In, Party Guid, Party Host Id, Party Host First Name, Party Host Last Name, Party Host Billing Address 1, Party Host Billing Address 2, Party Host Billing Town City, Party Host Billing Postcode");
                        
            // get all db rows from wonderlandDonation
            foreach (DonationRow donationRow in this.DatabaseContext.Database.Fetch<DonationRow>("SELECT * FROM wonderlandDonation ORDER BY [Timestamp] DESC"))
            {
                PartyHost partyHost = this.Members.GetPartyHost(donationRow.PartyGuid);

                if (partyHost == null)
                {
                    LogHelper.Error(typeof(ExportFinancials), "Party Host for " + donationRow.PartyGuid.ToString() + " not found", new Exception());
                }

                string email = string.Empty;
                if (donationRow.MemberId.HasValue)
                {
                    IPartier partier = ((IPartier)this.Members.GetById(donationRow.MemberId.Value));
                    if (partier != null)
                    {
                        email = partier.Email;
                    }
                }

                stringBuilder.AppendLine(
                    string.Join(
                        ", ",
                        new object[] {
                                donationRow.VPSTxId ?? string.Empty,                                                                                //  1. VpsTxId
                                donationRow.VendorTxCode,                                                                                           //  2. VendorTxCode
                                donationRow.Success ? "Y" : "N",                                                                                    //  3. Success
                                donationRow.MemberId,                                                                                               //  4. Member Id
                                email,                                                                                                              //  5. Donor Email
                                donationRow.FirstName.Replace(',', ' '),                                                                            //  6. Donor First Name
                                donationRow.LastName.Replace(',', ' '),                                                                             //  7. Donor Last Name
                                donationRow.Address1.Replace(',', ' '),                                                                             //  8. Donor Billing Address 1
                                donationRow.Address2 != null ? donationRow.Address2.Replace(',',' ') : string.Empty,                                //  9. Donor Billing Address 2
                                donationRow.TownCity.Replace(',', ' '),                                                                             // 10. Donor Billing Town City
                                donationRow.Postcode.Replace(',', ' '),                                                                             // 11. Donor Billing Postcode
                                donationRow.Amount,                                                                                                 // 12. Amount Donated
                                donationRow.Timestamp.Date,                                                                                         // 13. Date
                                donationRow.Timestamp.TimeOfDay,                                                                                    // 14. Time
                                donationRow.GiftAid ? "Y" : "N",                                                                                    // 15. Gift Aid
                                donationRow.PaymentJourney.ToString(),                                                                              // 16. Payment Journey
                                "Y",                                                                                                                // 17. Opt In
                                donationRow.PartyGuid,                                                                                              // 18. Party Guid
                                partyHost != null ? partyHost.Id : -1,                                                                              // 19. Party Host Id
                                partyHost != null ? partyHost.FirstName : string.Empty,                                                             // 20. Party Host First Name
                                partyHost != null ? partyHost.LastName : string.Empty,                                                              // 21. Party Host Last Name
                                partyHost != null ? partyHost.BillingAddress.Address1 : string.Empty,                                               // 22. Party Host Billing Address 1
                                partyHost != null ? partyHost.BillingAddress.Address2 : string.Empty,                                               // 23. Party Host Billing Address 2
                                partyHost != null ? partyHost.BillingAddress.TownCity : string.Empty,                                               // 24. Party Host Billing Town City
                                partyHost != null ? partyHost.BillingAddress.Postcode : string.Empty                                                // 25. Party Host Billing Postcode
                            })
                        );
            }

            // steream out the response
            this.Response.Clear();            
            this.Response.AddHeader("content-disposition", "attachment; filename=financials.csv");
            this.Response.ContentType = "text/csv";
            this.Response.ContentEncoding = Encoding.UTF8;
            this.Response.Write(stringBuilder.ToString());
            this.Response.End();
        }
    }
}