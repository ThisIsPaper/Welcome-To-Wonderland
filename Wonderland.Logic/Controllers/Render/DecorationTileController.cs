
namespace Wonderland.Logic.Controllers.Render
{
    using System.Web.Mvc;
    using Umbraco.Web.Models;

    public class DecorationTileController : BaseRenderMvcController
    {       
        public override ActionResult Index(RenderModel model)
        {
            // always redirect to partent
            return this.Redirect(model.Content.Parent.Url);
        }
    }
}
