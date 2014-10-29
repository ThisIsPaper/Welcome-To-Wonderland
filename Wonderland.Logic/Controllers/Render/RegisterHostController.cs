
namespace Wonderland.Logic.Controllers.Render
{
    using System.Web.Mvc;
    using Umbraco.Web.Models;
    using Wonderland.Logic.Extensions;
    using Wonderland.Logic.Models.Content;
    using Wonderland.Logic.Models.Members;

    public class RegisterHostController : BaseRenderMvcController
    {
        /// <summary>
        /// NOTE: specified controller folder, as this may have been triggered by the surface controller
        /// </summary>
        /// <param name="renderModel"></param>
        /// <returns></returns>
        public override ActionResult Index(RenderModel renderModel)
        {
            RegisterHost model = (RegisterHost)renderModel.Content;

            if (this.Members.IsLoggedInPartier())
            {
                if (this.Members.GetCurrentMember() is PartyHost)
                {
                    PartyHost partyHost = (PartyHost)this.Members.GetCurrentMember();

                    if (!partyHost.HasRequestedPartyKit)
                    {

                        return View("RegisterHost/RegisterHostPartyKit", model);
                    }

                    return this.Redirect(partyHost.PartyUrl);
                }
                else
                {
                    // user already registered but not a host, so redirect back to home
                    return this.Redirect(Home.GetCurrentHome(model).Url);
                }
            }
           
            return View("RegisterHost/RegisterHost", model); 
        }
    }
}
