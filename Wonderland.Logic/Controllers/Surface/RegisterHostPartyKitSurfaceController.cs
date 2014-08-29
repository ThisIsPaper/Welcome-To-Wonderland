
namespace Wonderland.Logic.Controllers.Surface
{
    using System.Linq;
    using System.Web.Mvc;
    using Umbraco.Web.Mvc;
    using Wonderland.Logic.Models.Content;
    using Wonderland.Logic.Models.Forms;

    public class RegisterHostPartyKitSurfaceController : SurfaceController
    {
        [ChildActionOnly]
        public ActionResult Index()
        {
            return this.PartialView("RegisterHostPartyKitFormPartial", new RegisterHostPartyKitForm());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterHostPartyKitForm(RegisterHostPartyKitForm registerHostPartyKitForm)
        {
            if (!ModelState.IsValid)
            {
                return this.CurrentUmbracoPage();
            }

            // TODO:

            return this.RedirectToUmbracoPage(this.CurrentPage.Parent.Children.Single(x => x.DocumentTypeAlias == RegisterHostPartyUrl.Alias));
        }
    }
}
