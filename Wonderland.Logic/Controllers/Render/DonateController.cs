
namespace Wonderland.Logic.Controllers.Render
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Umbraco.Web.Models;
    using Wonderland.Logic.Extensions;
    using Wonderland.Logic.Interfaces;
    using Wonderland.Logic.Models.Content;
    using Wonderland.Logic.Models.Database;

    public class DonateController : BaseRenderMvcController
    {
        public override ActionResult Index(RenderModel renderModel)
        {
            Donate model = (Donate)renderModel.Content;
            
            // if VendorTxCode can be found on the querystring, then get party guid from transaction table
            Guid vendorTxCode;
            if (Guid.TryParse(this.Request.QueryString["VendorTxCode"], out vendorTxCode))
            {
                DonationRow donationRow = this.DatabaseContext.Database.Fetch<DonationRow>("SELECT TOP 1 * FROM wonderlandDonation WHERE VendorTxCode = @0", vendorTxCode).SingleOrDefault();

                if (donationRow != null)
                {
                    model.PartyHost = this.Members.GetPartyHost(donationRow.PartyGuid);
                    model.DonationRow = donationRow;

                    if (donationRow.Success)
                    {
                        return this.View("Donate/Complete", model);
                    }
                    else
                    {
                        return this.View("Donate/Failed", model);
                    }
                }
                else
                {
                    return this.View("Donate/UnknownTransaction", model);
                }
            }

            Guid partyGuid;
            if (Guid.TryParse(this.Request.QueryString["partyGuid"], out partyGuid))
            {
                model.PartyHost = this.Members.GetPartyHost(partyGuid);
            }

            // if a party wasn't found via the query string, then attempt to find party associated with the current login 
            if (model.PartyHost == null && this.Members.IsLoggedInPartier())
            {
                model.PartyHost = this.Members.GetPartyHost(((IPartier)this.Members.GetCurrentMember()).PartyGuid);
            }

            if (model.PartyHost != null)
            {
                return this.View("Donate/Donate", model);
            }

            // couldn't find a party host for this donation, so go to Macmillan
            return this.View("Donate/Macmillan", model);
        }
    }
}
