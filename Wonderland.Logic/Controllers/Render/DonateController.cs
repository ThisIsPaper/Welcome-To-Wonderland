
namespace Wonderland.Logic.Controllers.Render
{
    using System;
    using System.Web.Mvc;
    using Umbraco.Web.Models;
    using Wonderland.Logic.Enums;
    using Wonderland.Logic.Extensions;
    using Wonderland.Logic.Interfaces;
    using Wonderland.Logic.Models.Content;
    using Wonderland.Logic.Models.Members;

    public class DonateController : BaseRenderMvcController
    {
        public override ActionResult Index(RenderModel renderModel)
        {
            Donate model = (Donate)renderModel.Content;

            // party host to associate donation with
            PartyHost partyHost = null;

            Guid partyGuid;
            if (Guid.TryParse(this.Request.QueryString["partyGuid"], out partyGuid))
            {
                partyHost = this.Members.GetPartyHost(partyGuid);
            }

            // if a party wasn't found via the query string, then attempt to find party associated with the current login 
            if (partyHost == null && this.Members.IsLoggedIn())
            {
                partyHost = this.Members.GetPartyHost(((IPartier)this.Members.GetCurrentMember()).PartyGuid);
            }

            if (partyHost != null)
            {
                model.PartyHost = partyHost;

                return this.View("Donate", model);
            }

            // couldn't find a party host for this donation, so go to Macmillan
            return this.View("Macmillan", model);
        }
    }
}
