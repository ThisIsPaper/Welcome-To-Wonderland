
namespace Wonderland.Logic.Controllers.Render
{
    using System.Web.Mvc;

    public class FashionTileController : BaseRenderMvcController
    {
        public override ActionResult Index(Umbraco.Web.Models.RenderModel model)
        {
            // always redirect to partent
            return this.Redirect(model.Content.Parent.Url);
        }
    }
}
