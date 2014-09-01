
namespace Wonderland.Logic.Controllers.Render
{
    using System.Linq;
    using System.Web.Mvc;
    using Umbraco.Web.Mvc;
    using Wonderland.Logic.Models.Content;
    using Wonderland.Logic.Models.Members;

    public class RegisterHostController : RenderMvcController
    {
        public ActionResult RegisterHost()
        {
            RegisterHost model = CurrentPage as RegisterHost;

            // safety checks incase user manually goes to this url
            if (this.User.Identity.IsAuthenticated)
            {
                if (this.User.IsInRole(Partier.HostRoleAlias))
                {
                    return this.Redirect(model.Children.Single(x => x.DocumentTypeAlias == RegisterHostPartyKit.Alias).Url);
                }
                else
                {
                    return this.Redirect(Home.GetCurrentHome(model).Url);
                }
            }

            return this.CurrentTemplate(model);
        }
    }
}
