
namespace Wonderland.Logic.Controllers.Surface
{
    using System.Web.Mvc;
    using System.Web.Security;
    using Umbraco.Core;
    using Umbraco.Core.Security;
    using Umbraco.Web.Mvc;
    using Wonderland.Logic.Models.Forms;

    public class LoginSurfaceController : SurfaceController
    {
        /// <summary>
        /// returns the partial for the login form
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult Index()
        {
            return this.PartialView("LoginFormPartial", new LoginForm());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoginForm(LoginForm loginForm)
        {
            if (!this.ModelState.IsValid)
            {
                return this.CurrentUmbracoPage();
            }

            if (Membership.ValidateUser(loginForm.EmailAddress, loginForm.Password))
            {
                // send cookie
                FormsAuthentication.SetAuthCookie(loginForm.EmailAddress, true);

                return this.Redirect("/");
            }
            else
            {
                this.ModelState.AddModelError("LoginValidation", "Invalid login");

                return this.CurrentUmbracoPage();
            }
        }
    }
}

