
namespace Wonderland.Logic.Controllers.Surface
{
    using System.Collections.Generic;
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
            // get the model for the current page
            RegisterHost registerHost = (RegisterHost)this.CurrentPage;

            // add marketing sources so view can build drop down 
            List<string> marketingSources = new List<string>(registerHost.MarketingSources);
            marketingSources.Insert(0, string.Empty);

            this.ViewBag.MarketingSources = new SelectList(marketingSources);

            return this.PartialView("RegisterHostFormPartial", new RegisterHostForm());
        }

        public JsonResult ValidateIsEmailAvailable(string emailAddress)
        {
            return Json(this.IsEmailAvailable(emailAddress), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterHostForm(RegisterHostForm registerHostForm)
        {
            if (!ModelState.IsValid)
            {
                return this.CurrentUmbracoPage();
            }

            if (!this.IsEmailAvailable(registerHostForm.EmailAddress))
            {
                this.ModelState.AddModelError("EmailValidation", "Email already registered");
                return this.CurrentUmbracoPage();
            }

            // TODO: create new member and login

                                    
            // have to redirect as next form has a differnt main view (it doesn't have the header / footer)
            return this.RedirectToUmbracoPage(this.CurrentPage.Children.Single(x => x.DocumentTypeAlias == RegisterHostPartyKit.Alias));
        }

        private bool IsEmailAvailable(string emailAddress)
        {            
            return this.Members.GetByUsername(emailAddress) == null;
        }
    }
}
