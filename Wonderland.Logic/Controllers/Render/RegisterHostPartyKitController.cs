
namespace Wonderland.Logic.Controllers.Render
{
    using System.Web.Mvc;
    using Wonderland.Logic.Models.Content;
    using Wonderland.Logic.Models.Members;

    public class RegisterHostPartyKitController : BaseRenderController
    {
        public ActionResult RegisterHostPartyKit()
        {
            RegisterHostPartyKit model = (RegisterHostPartyKit)this.CurrentPage;

            if (this.Members.IsLoggedIn() && this.Members.GetCurrentMember() is PartyHost)
            {
                // only allow access to this page if the host hasn't yet requested a party kit
                if (!((PartyHost)this.Members.GetCurrentMember()).HasRequestedPartyKit)
                {
                    return this.CurrentTemplate(model);
                }
            }
            
            // fallback
            return this.Redirect(Home.GetCurrentHome(model).Url);
        }
    }
}
