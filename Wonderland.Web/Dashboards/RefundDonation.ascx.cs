

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

    public partial class RefundDonation : BaseUserControl
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.refundDonationButton.Click += this.RefundDonationButton_Click;
        }

        private void RefundDonationButton_Click(object sender, EventArgs e)
        {
            Guid vendorTxCode;
            if (Guid.TryParse(this.vendorTxCodeTextBox.Text, out vendorTxCode))
            {
                DonationRow donationRow = this.DatabaseContext.Database.SingleOrDefault<DonationRow>(vendorTxCode);
                
                if (donationRow != null)
                {
                    donationRow.Success = false;
                    donationRow.Cancelled = false;
                    donationRow.Refunded = true;

                    this.DatabaseContext.Database.Update(donationRow);

                    this.resultLiteral.Text = "Refunded: " + this.vendorTxCodeTextBox.Text;
                    this.vendorTxCodeTextBox.Text = string.Empty;
                }
                else
                {
                    this.resultLiteral.Text = "Unknown VendorTxCode";
                }
            }
            else
            {
                this.resultLiteral.Text = "Invalid VendorTxCode";
            }
        }
    }
}