
namespace Wonderland.Logic.Controllers.Surface
{
    using System;
    using System.Net.Mail;
    using System.Web.Mvc;
    using Umbraco.Web.Mvc;
    using Wonderland.Logic.Interfaces;
    using Wonderland.Logic.Models.Content;
    using Wonderland.Logic.Models.Forms;

    public class ForgottenPasswordSurfaceController : SurfaceController
    {
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
                Guid forgottenPasswordGuid = Guid.NewGuid();

                partier.ForgottenPasswordGuid = forgottenPasswordGuid;

                ForgottenPassword forgottenPassword = (ForgottenPassword)this.CurrentPage;

                MailMessage mailMessage = new MailMessage();

                mailMessage.From = new MailAddress(forgottenPassword.ServerEmailAddress);
                mailMessage.To.Add(new MailAddress(partier.Email));
                mailMessage.Subject = forgottenPassword.EmailSubject;
                mailMessage.IsBodyHtml = true;

                mailMessage.Body = forgottenPassword.EmailBody.Replace("[%RESET_PASSWORD_LINK%]", this.Request.Url.Scheme + "://" + this.Request.Url.Host.ToLower() + this.Umbraco.TypedContentSingleAtXPath("//" + ResetPassword.Alias).Url + "?forgottenPasswordGuid=" + forgottenPasswordGuid.ToString("D"));

                using (SmtpClient smtpClient = new SmtpClient())
                {
                    smtpClient.Send(mailMessage);
                }

            }

            return this.View("ForgottenPassword/Complete", this.CurrentPage);
        }
    }
}
