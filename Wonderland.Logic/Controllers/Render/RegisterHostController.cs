
namespace Wonderland.Logic.Controllers.Render
{
    using System.Linq;
    using System.Web.Mvc;
    using Umbraco.Web.Models;
    using Wonderland.Logic.Models.Content;
    using Wonderland.Logic.Models.Members;

    public class RegisterHostController : BaseRenderMvcController
    {
        public override ActionResult Index(RenderModel renderModel)
        {
            RegisterHost model = (RegisterHost)renderModel.Content;

            if (this.Members.IsLoggedIn())
            {
                if (this.Members.GetCurrentMember() is PartyHost)
                {
                    PartyHost partyHost = (PartyHost)this.Members.GetCurrentMember();

                    if (!partyHost.HasRequestedPartyKit)
                    {
                        return View("RegisterHostPartyKit", model);
                    }

                    // TODO: flag so that the final view can only be rendered once
                    return View("RegisterHostPartyUrl", model);
                }
                else
                {
                    // user already registered but not a host, so redirect back to home
                    return this.Redirect(Home.GetCurrentHome(model).Url);
                }
            }
           
            return View("RegisterHost", model);
        }
    }
}
