
namespace Wonderland.Logic.Controllers.Render
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Umbraco.Core.Models;
    using Umbraco.Web.Models;
    using Wonderland.Logic.Models.Content;
    using Wonderland.Logic.Models.Members;
    using Wonderland.Logic.Extensions;

    public class PartyController : BaseRenderMvcController
    {
        public override ActionResult Index(RenderModel renderModel) // have to override index instead of a Party() method, as there isn't a Party template
        {
            Party model = (Party)renderModel.Content;

            PartyHost partyHost;

            #region Redirect if party host unknown (see: Events/SetPartyContentFinder)

            try
            {
                partyHost = (PartyHost)this.Members.GetById(int.Parse(this.HttpContext.Items["partyHostId"].ToString()));
            }
            catch
            {
                partyHost = null;
            }

            if (!(partyHost is PartyHost))
            {
                if (this.Members.IsLoggedIn())
                {                    
                    return this.Redirect(((IPartier)this.Members.GetCurrentMember()).PartyUrl);
                }

                // fallback
                return this.Redirect(Home.GetCurrentHome(model).Url);
            }

            #endregion

            // known host, so build the renderModel renderModel
            model.PartyHost = partyHost;

            // image

            // location - use model.PartyHost.PartyAddress
            
            // heading - use custom heading if set by host
            if (!string.IsNullOrWhiteSpace(partyHost.PartyHeading))
            {
                model.Heading = partyHost.PartyHeading;
            }
            
            // copy - use custom copy if set by host
            if (!string.IsNullOrWhiteSpace(partyHost.PartyCopy))
            {
                model.Copy = partyHost.PartyCopy;
            }

            // wall - TODO: hit db ?

            // totaliser

            //// partiers - the host + all guests            
            model.Partiers = this.Members.GetPartiers(partyHost.PartyGuid);

            // badges

            // determine view to return
            if (model.PartyHost.Blocked)
            {
                return View("Blocked", model);
            }

            if (this.Members.IsLoggedIn())
            {
                IPartier partier = (IPartier)this.Members.GetCurrentMember();
                if (partier is PartyHost && partier.Id == partyHost.Id)
                {
                    return View("Host", model);
                }
                else if (partier is PartyGuest && ((PartyGuest)partier).PartyGuid == partyHost.PartyGuid)
                {
                    return View("Guest", model);
                }
            }

            return View("Anonymous", model);
        }
    }
}
