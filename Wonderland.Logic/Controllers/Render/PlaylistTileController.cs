
namespace Wonderland.Logic.Controllers.Render
{
    using System.Web.Mvc;
    using Umbraco.Web.Models;

    public class PlaylistTileController : BaseRenderMvcController
    {
        public override ActionResult Index(RenderModel renderModel)
        {
            // always redirect to partent
            return this.Redirect(renderModel.Content.Parent.Url);
        }
    }
}
