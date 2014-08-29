
namespace Wonderland.Logic.Controllers.Surface
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Umbraco.Web.Mvc;
    using Wonderland.Logic.Models.Content;
    using Wonderland.Logic.Models.Forms;
    using Wonderland.Logic.Models.Members;

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
            return Json(this.Members.GetByUsername(emailAddress) == null, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterHostForm(RegisterHostForm registerHostForm)
        {
            if (!ModelState.IsValid)
            {
                return this.CurrentUmbracoPage();
            }

            // safety check here as the Umbraco membership service allows the creation of duplicate users !
            if (this.Members.GetByUsername(registerHostForm.EmailAddress) != null)
            {
                this.ModelState.AddModelError("EmailValidation", "Email already registered");
                return this.CurrentUmbracoPage();
            }

            Partier.RegisterHost(registerHostForm.EmailAddress, registerHostForm.Password, registerHostForm.MarketingSource);
        
            // redirecting as next form has a different main view (it doesn't have the header / footer)
            return this.RedirectToUmbracoPage(this.CurrentPage.Children.Single(x => x.DocumentTypeAlias == RegisterHostPartyKit.Alias));
        }      
    }
}
