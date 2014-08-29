
namespace Wonderland.Logic.Controllers.Surface
{
    using System.Linq;
    using System.Web.Mvc;
    using Umbraco.Web.Mvc;
    using Wonderland.Logic.Models.Content;
    using Wonderland.Logic.Models.Forms;

    public class RegisterHostSurfaceController : SurfaceController
    {
        [ChildActionOnly]
        public ActionResult Index()
        {
            return this.PartialView("RegisterHostFormPartial", new RegisterHostForm());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterHostForm(RegisterHostForm registerHostForm)
        {
            if (!ModelState.IsValid)
            {
                return this.CurrentUmbracoPage();
            }

            // TODO: check and create the member...
                        
            
            // have to redirect as next form has a differnt main view (it doesn't have the header / footer)
            return this.RedirectToUmbracoPage(this.CurrentPage.Children.Single(x => x.DocumentTypeAlias == RegisterHostPartyKit.Alias));
        }
    }
}
