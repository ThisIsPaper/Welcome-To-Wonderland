
namespace Wonderland.Web.Dashboards
{
    using System;
    using System.Text;
    using Umbraco.Core;
    using Umbraco.Web;
    using Umbraco.Web.Security;
    using Wonderland.Logic.Models.Database;
    using Wonderland.Logic.Enums;
    using Wonderland.Logic.Extensions;
    using Wonderland.Logic.Interfaces;
    using Wonderland.Logic.Models.Members;

    public partial class AddDonation : BaseUserControl
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.addDonationButton.Click += this.AddDonationButton_Click;
        }

        private void AddDonationButton_Click(object sender, EventArgs e)
        {
            Guid partyGuid;
            if (Guid.TryParse(this.partyGuidTextBox.Text, out partyGuid))
            {
                // is the party known ?
                PartyHost partyHost = this.Members.GetPartyHost(partyGuid);

                if (partyHost != null)
                {
                    decimal amount;
                    if (decimal.TryParse(this.amountTextBox.Text, out amount))
                    {
                        // add row to wonderland donation
                        this.DatabaseContext.Database.Insert(
                             new DonationRow()
                             {
                                 PartyGuid = partyGuid,
                                 Amount = amount,
                                 GiftAid = this.giftAidCheckBox.Checked,
                                 MemberId = partyHost.Id,
                                 FirstName = partyHost.FirstName,
                                 LastName = partyHost.LastName,
                                 Address1 = partyHost.BillingAddress.Address1,
                                 Address2 = partyHost.BillingAddress.Address2,
                                 TownCity = partyHost.BillingAddress.TownCity,
                                 Postcode = partyHost.BillingAddress.Postcode,
                                 PaymentJourney = PaymentJourney.Manual,
                                 Success = true
                             }
                        );

                        this.partyGuidTextBox.Text = string.Empty;
                        this.amountTextBox.Text = string.Empty;
                        this.resultLiteral.Text = "Added: " + amount.ToString() + " to Party: " + partyGuid.ToString();

                    }
                    else
                    {
                        this.resultLiteral.Text = "Invalid Amount";
                    }
                }
                else
                {
                    this.resultLiteral.Text = "Unknown Party";
                }
            }
            else
            {
                this.resultLiteral.Text = "Invalid Party Guid";
            }
        }
    }
}