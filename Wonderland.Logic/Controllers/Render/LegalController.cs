
namespace Wonderland.Logic.Controllers.Render
{
    using System.Web.Mvc;
    using Wonderland.Logic.Models.Content;

    public class LegalController : BaseRenderMvcController
    {
        public ActionResult Legal() // method executed in preference to the Index, as Legal is the template name
        {
            Legal model = (Legal)this.CurrentPage;

            if (!string.IsNullOrWhiteSpace(model.ExternalRedirect))
            {
                return this.Redirect(model.ExternalRedirect);
            }

            return this.CurrentTemplate(model);
        }
    }
}
