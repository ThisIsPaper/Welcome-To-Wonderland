
namespace Wonderland.Logic.Controllers.Render
{
    using System.Web.Mvc;
    using Umbraco.Web.Models;
    using Wonderland.Logic.Extensions;
    using Wonderland.Logic.Interfaces;
    using Wonderland.Logic.Models.Content;
    using Wonderland.Logic.Models.Members;

    public class PartyController : BaseRenderMvcController
    {
        public override ActionResult Index(RenderModel renderModel) // have to override index instead of a Party() method, as there isn't a Party template
        {
            Party model = (Party)renderModel.Content;

            PartyHost partyHost;

            try
            {
                partyHost = (PartyHost)this.Members.GetById(int.Parse(this.HttpContext.Items["partyHostId"].ToString()));
            }
            catch
            {
                partyHost = null;
            }

            // party host not found on query string, so
            if (!(partyHost is PartyHost))
            {
                if (this.Members.IsLoggedInPartier())
                {                    
                    return this.Redirect(((IPartier)this.Members.GetCurrentMember()).PartyUrl);
                }

                // fallback to login
                return this.Redirect(model.SiteSettings.LoginUrl);
            }

            // known host, so set the party host for this cms page, and build the model
            model.PartyHost = partyHost;
            
            model.Heading = partyHost.PartyHeading;

            model.Copy = partyHost.PartyCopy;

            // partiers - the host + all guests            
            model.Partiers = this.Members.GetPartiers(partyHost.PartyGuid);

            // urls with guids
            model.DonateUrl = this.Umbraco.TypedContentSingleAtXPath("//" + Donate.Alias).Url + "?partyGuid=" + partyHost.PartyGuid.ToString();
            model.RegisterGuestUrl = this.Umbraco.TypedContentSingleAtXPath("//" + RegisterGuest.Alias).Url + "?partyGuid=" + partyHost.PartyGuid.ToString();

            // determine view to return
            if (model.PartyHost.Blocked)
            {
                return View("Blocked", model);
            }

            if (this.Members.IsLoggedInPartier())
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
