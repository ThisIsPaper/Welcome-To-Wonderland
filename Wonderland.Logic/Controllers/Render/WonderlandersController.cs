
namespace Wonderland.Logic.Controllers.Render
{
    using System.Web.Mvc;
    using Umbraco.Web.Models;
    using Wonderland.Logic.Models.Content;

    public class WonderlandersController : BaseRenderMvcController
    {
        // use this hijack controller for output caching
        public override ActionResult Index(RenderModel renderModel)
        {
            Wonderlanders model = (Wonderlanders)renderModel.Content;
            
            return this.CurrentTemplate(model);
        }
    }
}
