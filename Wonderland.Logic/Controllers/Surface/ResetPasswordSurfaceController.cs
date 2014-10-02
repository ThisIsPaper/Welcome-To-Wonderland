
namespace Wonderland.Logic.Controllers.Surface
{
    using System;
    using System.Web.Mvc;
    using Umbraco.Web.Mvc;
    using Wonderland.Logic.Interfaces;
    using Wonderland.Logic.Models.Content;
    using Wonderland.Logic.Models.Forms;
    using Wonderland.Logic.Extensions;
    using Umbraco.Core.Models;

    public class ResetPasswordSurfaceController : SurfaceController
    {
        [ChildActionOnly]
        [MemberAuthorize]
        public ActionResult RenderResetPasswordForm()
        {
            return this.PartialView("ResetPassword/Forms/ResetPasswordForm", new ResetPasswordForm());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [MemberAuthorize]
        public ActionResult HandleResetPasswordForm(ResetPasswordForm resetPasswordForm)
        {
            if (!this.ModelState.IsValid)
            {
                return this.CurrentUmbracoPage();
            }

            IPartier partier = this.Members.GetCurrentPartier();
            if (partier != null)
            {
                IMember member = this.Services.MemberService.GetById(partier.Id);
                if (member != null)
                {
                    this.Services.MemberService.SavePassword(member, resetPasswordForm.Password);
                }
            }

            return this.View("ResetPassword/Complete", this.CurrentPage);
        }
    }
}
