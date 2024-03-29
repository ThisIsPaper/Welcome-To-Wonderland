﻿
namespace Wonderland.Logic.Controllers.Render
{
    using System.Web.Mvc;
    using Umbraco.Web.Models;
    using Wonderland.Logic.Enums;
    using Wonderland.Logic.Extensions;
    using Wonderland.Logic.Models.Content;

    public class WonderlandersController : BaseRenderMvcController
    {
        //[OutputCache(Duration=SitePage.CacheDuration)]
        public override ActionResult Index(RenderModel renderModel)
        {
            Wonderlanders model = (Wonderlanders)renderModel.Content;
            
            model.TopFundraisers = this.Members.GetTopPartyHosts(LeaderboardType.TopFundraisers, model.TopFundraisersCount);
            model.MostGuests = this.Members.GetTopPartyHosts(LeaderboardType.MostGuests, model.MostGuestsCount);

            return this.CurrentTemplate(model);
        }
    }
}
