
namespace Wonderland.Logic.Controllers.Surface
{
    using System.Web.Mvc;
    using Umbraco.Web.Mvc;
    using Wonderland.Logic.Models.Forms;
    using Wonderland.Logic.Models.Members;
    using Wonderland.Logic.Models.Content;

    public class LoginSurfaceController : SurfaceController
    {
        /// <summary>
        /// Handles inbound links to the login page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult NavigateToLoginUrl()
        {
            return this.Redirect(this.Umbraco.TypedContentSingleAtXPath("//" + Login.Alias).Url);
        }

        /// <summary>
        /// returns the partial for the login form
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult RenderLoginForm()
        {
            return this.PartialView("LoginFormPartial", new LoginForm());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HandleLoginForm(LoginForm loginForm)
        {
            if (!this.ModelState.IsValid)
            {
                return this.CurrentUmbracoPage();
            }

            // this .Login check sets the forms authentication cookie if successful
            if (this.Members.Login(loginForm.EmailAddress, loginForm.Password))            
            {
                return this.Redirect(((IPartier)this.Members.GetByUsername(loginForm.EmailAddress)).GetPartyUrl());
            }
            else
            {
                this.ModelState.AddModelError("LoginValidation", "Invalid login");

                return this.CurrentUmbracoPage();
            }
        }
    }
}

