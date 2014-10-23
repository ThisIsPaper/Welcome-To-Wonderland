

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

    public partial class DeleteDonation : BaseUserControl
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.deleteDonationButton.Click += this.DeleteDonationButton_Click;
        }

        private void DeleteDonationButton_Click(object sender, EventArgs e)
        {   
            Guid vendorTxCode;
            if (Guid.TryParse(this.vendorTxCodeTextBox.Text, out vendorTxCode))
            {
                int result = this.DatabaseContext.Database.Delete(new DonationRow() { VendorTxCode = vendorTxCode });

                if (result == 1)
                {
                    this.Cache.Remove("TotalAmountRaised");

                    this.resultLiteral.Text = "Deleted: " + this.vendorTxCodeTextBox.Text;
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