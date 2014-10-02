
namespace Wonderland.Logic.Controllers.Surface
{
    using System.Web.Mvc;
    using Umbraco.Web.Mvc;
    using Wonderland.Logic.Interfaces;
    using Wonderland.Logic.Models.Forms;

    public class LoginSurfaceController : SurfaceController
    {
        /// <summary>
        /// returns the partial for the login form
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult RenderLoginForm()
        {
            return this.PartialView("Login/Forms/LoginForm", new LoginForm());
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
                return this.Redirect(((IPartier)this.Members.GetByUsername(loginForm.EmailAddress)).PartyUrl);
            }
            else
            {
                this.ModelState.AddModelError("LoginValidation", "Invalid login");

                return this.CurrentUmbracoPage();
            }
        }
    }
}

