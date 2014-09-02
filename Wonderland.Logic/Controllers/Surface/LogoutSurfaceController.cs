
namespace Wonderland.Logic.Controllers.Surface
{
    using System.Web.Mvc;
    using System.Web.Security;
    using Umbraco.Web.Mvc;

    public class LogoutSurfaceController : SurfaceController
    {
        [HttpGet]
        [Authorize]
        public ActionResult Logout()
        {
            this.Members.Logout();
            
            return this.Redirect("/");
        }
    }
}
