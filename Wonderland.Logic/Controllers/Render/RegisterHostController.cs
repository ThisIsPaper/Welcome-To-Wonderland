
namespace Wonderland.Logic.Controllers.Render
{
    using System.Linq;
    using System.Web.Mvc;
    using Wonderland.Logic.Models.Content;
    using Wonderland.Logic.Models.Members;

    public class RegisterHostController : BaseRenderMvcController
    {
        public ActionResult RegisterHost()
        {            
            RegisterHost registerHost = (RegisterHost)this.CurrentPage;

            if (this.Members.IsLoggedIn())
            {
                if (this.Members.GetCurrentMember() is PartyHost)
                {
                    // user already registered as a host, so move onto the next step
                    return this.Redirect(registerHost.Children.Single(x => x.DocumentTypeAlias == RegisterHostPartyKit.Alias).Url);
                }
                else
                {
                    // user already registered but not a host, so redirect back to home
                    return this.Redirect(Home.GetCurrentHome(registerHost).Url);
                }
            }

            return this.CurrentTemplate(registerHost);
        }
    }
}
