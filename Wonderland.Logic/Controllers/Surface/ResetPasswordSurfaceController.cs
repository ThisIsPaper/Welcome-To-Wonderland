
namespace Wonderland.Logic.Controllers.Surface
{
    using System;
    using System.Web.Mvc;
    using Umbraco.Web.Mvc;
    using Wonderland.Logic.Interfaces;
    using Wonderland.Logic.Models.Content;
    using Wonderland.Logic.Models.Forms;

    public class ResetPasswordSurfaceController : SurfaceController
    {
        [ChildActionOnly]
        public ActionResult RenderResetPasswordForm()
        {
            return this.PartialView("ResetPasswordForm", new ResetPasswordForm());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HandleResetPasswordForm(ResetPasswordForm resetPasswordForm)
        {
            if (!this.ModelState.IsValid)
            {
                return this.CurrentUmbracoPage();
            }

            return this.View("ResetPassword/Complete", this.CurrentPage);
        }
    }
}
