
namespace Wonderland.Logic.Controllers.Render
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Wonderland.Logic.Models.Content;
    using Wonderland.Logic.Models.Members;

    public class RegisterGuestController : BaseRenderMvcController
    {
        public ActionResult RegisterGuest()
        {
            RegisterGuest model = (RegisterGuest)this.CurrentPage;

            // check to see there's a valid party guid
            Guid partyGuid;

            if (Guid.TryParse(this.Request.QueryString["partyGuid"], out partyGuid))
            {
                // TODO: ensure guid is a valid party guid

                return this.CurrentTemplate(model);    
            }

            // fallback
            return this.Redirect("/");            
        }
    }
}
