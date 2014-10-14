
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

    public partial class ExportFinancials : BaseUserControl
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.exportFinancialsButton.Click += this.ExportFinancialsButton_Click;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        private void ExportFinancialsButton_Click(object sender, EventArgs e)
        {
            StringBuilder stringBuilder = new StringBuilder();

            // headings
            stringBuilder.AppendLine("VendorTxCode, Party Guid, Party Host Email, Amount, Gift Aid, Member Email, First Name, Last Name, Address 1, Address 2, Town City, Postcode, Timestamp, VPSTxID, Success");

            // get all db rows from wonderlandDonation
            foreach (DonationRow donationRow in this.DatabaseContext.Database.Fetch<DonationRow>("SELECT * FROM wonderlandDonation ORDER BY [Timestamp] DESC"))
            {
                stringBuilder.AppendLine(
                    string.Join(", ",
                        new object[] {
                            donationRow.VendorTxCode,
                            donationRow.PartyGuid,
                            this.Members.GetPartyHost(donationRow.PartyGuid).Email,
                            donationRow.Amount,
                            donationRow.GiftAid,
                            donationRow.MemberId == null ? "Anonymous" : ((IPartier)this.Members.GetById(donationRow.MemberId.Value)).Email,
                            donationRow.FirstName,
                            donationRow.LastName,
                            donationRow.Address1,
                            donationRow.Address2,
                            donationRow.TownCity,
                            donationRow.Postcode,
                            donationRow.Timestamp,
                            donationRow.VPSTxId,
                            donationRow.Success
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