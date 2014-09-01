
namespace Wonderland.Logic.Controllers.Surface
{
    using System.Linq;
    using System.Web.Mvc;
    using Umbraco.Web.Mvc;
    using Wonderland.Logic.Models.Content;
    using Wonderland.Logic.Models.Forms;
    using Wonderland.Logic.Models.Members;

    public class RegisterHostPartyKitSurfaceController : SurfaceController
    {
        [ChildActionOnly]
        [Authorize(Roles = Partier.HostRoleAlias)]
        public ActionResult Index()
        {
            return this.PartialView("RegisterHostPartyKitFormPartial", new RegisterHostPartyKitForm());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Partier.HostRoleAlias)]
        public ActionResult RegisterHostPartyKitForm(RegisterHostPartyKitForm registerHostPartyKitForm)
        {
            if (!ModelState.IsValid)
            {
                return this.CurrentUmbracoPage();
            }
            
            // Partier partier = Partier.GetPartier(); // no need to null check as only logged in members can get here
            
            // TODO:
            


            return this.RedirectToUmbracoPage(this.CurrentPage.Parent.Children.Single(x => x.DocumentTypeAlias == RegisterHostPartyUrl.Alias));
        }
    }
}
