
namespace Wonderland.Logic.Controllers.Render
{
    using System.Web.Mvc;
    using Umbraco.Web.Models;
    using Wonderland.Logic.Enums;
    using Wonderland.Logic.Extensions;
    using Wonderland.Logic.Interfaces;
    using Wonderland.Logic.Models.Content;
    using Wonderland.Logic.Models.Members;

    public class WonderlandersController : BaseRenderMvcController
    {
        // use this hijack controller for output caching
        public override ActionResult Index(RenderModel renderModel)
        {
            Wonderlanders model = (Wonderlanders)renderModel.Content;

            //model.TopFundraisers = this.Members.GetTopPartyHosts(LeaderboardType.TopFundraisers, 10);
            //model.MostGuests = this.Members.GetTopPartyHosts(LeaderboardType.MostGuests, 10);
            
            return this.CurrentTemplate(model);
        }
    }
}
