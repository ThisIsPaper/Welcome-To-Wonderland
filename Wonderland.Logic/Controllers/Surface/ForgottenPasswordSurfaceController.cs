
namespace Wonderland.Logic.Controllers.Surface
{
    using System;
    using System.Web.Mvc;
    using Umbraco.Web.Mvc;
    using Wonderland.Logic.Interfaces;
    using Wonderland.Logic.Models.Content;
    using Wonderland.Logic.Models.Forms;

    public class ForgottenPasswordSurfaceController : SurfaceController
    {
        [HttpGet]
        public ActionResult NavigateToForgottenPasswordUrl()
        {
            return this.Redirect(this.Umbraco.TypedContentSingleAtXPath("//" + ForgottenPassword.Alias).Url);
        }

        [ChildActionOnly]
        public ActionResult RenderForgottenPasswordForm()
        {
            return this.PartialView("ForgottenPasswordForm", new ForgottenPasswordForm());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HandleForgottenPasswordForm(ForgottenPasswordForm forgottenPasswordForm)
        {
            if (!this.ModelState.IsValid)
            {
                return this.CurrentUmbracoPage();
            }

            IPartier partier = this.Members.GetByUsername(forgottenPasswordForm.EmailAddress) as IPartier;

            if (partier != null)
            {
                // set forgotten password guid and send email
            }

            return this.View("ForgottenPassword/Complete", this.CurrentPage);
        }
    }
}
