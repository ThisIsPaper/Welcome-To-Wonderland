
namespace Wonderland.Logic.Controllers.Render
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Umbraco.Web.Models;
    using Wonderland.Logic.Extensions;
    using Wonderland.Logic.Models.Content;
    using Wonderland.Logic.Models.Database;
    using Wonderland.Logic.Models.Members;

    public class RegisterGuestController : BaseRenderMvcController
    {
        public override ActionResult Index(RenderModel renderModel)        
        {
            RegisterGuest model = (RegisterGuest)renderModel.Content;

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
                        return this.View("RegisterGuest/Complete", model);
                    }
                    else
                    {
                        return this.View("RegisterGuest/Failed", model);
                    }                    
                }
                else
                {
                    return this.View("RegisterGuest/UnknownTransaction", model);
                }
            }

            Guid partyGuid;
            if (Guid.TryParse(this.Request.QueryString["partyGuid"], out partyGuid))
            {                
                model.PartyHost = this.Members.GetPartyHost(partyGuid);

                if (model.PartyHost != null)
                {
                    if (this.Members.IsLoggedInPartier())
                    {
                        if (this.Members.GetCurrentMember() is PartyGuest)
                        {
                            PartyGuest partyGuest = (PartyGuest)this.Members.GetCurrentMember();

                            return this.View("RegisterGuest/RegisterGuestBilling", model);
                        }
                        else
                        {
                            return this.Redirect(Home.GetCurrentHome(model).Url);
                        }
                    }

                    return View("RegisterGuest/RegisterGuest", model);
                }
            }

            // fallback
            return this.Redirect(Home.GetCurrentHome(model).Url);       
        }
    }
}
