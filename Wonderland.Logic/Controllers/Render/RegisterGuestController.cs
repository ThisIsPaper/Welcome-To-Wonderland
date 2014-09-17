
namespace Wonderland.Logic.Controllers.Render
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Umbraco.Web.Models;
    using Wonderland.Logic.Extensions;
    using Wonderland.Logic.Models.Content;
    using Wonderland.Logic.Models.Members;

    public class RegisterGuestController : BaseRenderMvcController
    {
        public override ActionResult Index(RenderModel renderModel)        
        {
            RegisterGuest model = (RegisterGuest)renderModel.Content;

            Guid partyGuid;

            if (Guid.TryParse(this.Request.QueryString["partyGuid"], out partyGuid))
            {
                // TODO: swap over to using the membership helper
                // model.PartyHost = this.Members.GetPartyHost(partyGuid);

                model.PartyHost = PartyHost.GetByPartyGuid(partyGuid);

                if (model.PartyHost != null)
                {
                    if (this.Members.IsLoggedIn())
                    {
                        if (this.Members.GetCurrentMember() is PartyGuest)
                        {
                            // TODO: check to see if they have done the payment step

                            return View("RegisterGuest/RegisterGuestConfirmation", model);
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
