
namespace Wonderland.Logic.Controllers.Render
{
    using System.Web.Mvc;
    using Wonderland.Logic.Extensions;
    using Wonderland.Logic.Models.Content;
    
    public class ProfileController : BaseRenderMvcController
    {
        public ActionResult MyProfile() // method executed in preference to the Index, as Home is the template name
        {
            Profile model = (Profile)this.CurrentPage;

            if(!this.Members.IsLoggedInPartier())
            {
                return this.Redirect(Home.GetCurrentHome(model).Url);
            }

            return this.CurrentTemplate(model);
        }
    }
}
