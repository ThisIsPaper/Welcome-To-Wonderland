
namespace Wonderland.Logic.Controllers.Render
{
    using System.Linq;
    using System.Web.Mvc;
    using Umbraco.Web.Mvc;
    using Wonderland.Logic.Models.Content;
    using Wonderland.Logic.Models.Members;

    public class RegisterHostPartyKitController : RenderMvcController
    {
        public ActionResult RegisterHostPartyKit()
        {
            RegisterHostPartyKit model = (RegisterHostPartyKit)this.CurrentPage;

            Partier partier = Partier.GetCurrentPartier();
            if (partier != null && !partier.HasRequestedPartyKit) // TODO: check to ensure a host
            {
                return this.CurrentTemplate(model);
            }

            // fallback
            return this.Redirect(Home.GetCurrentHome(model).Url);
        }
    }
}
