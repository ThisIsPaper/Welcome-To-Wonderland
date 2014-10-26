
namespace Wonderland.Logic.Controllers.Render
{
    using System;
    using System.Collections.Generic;
    using System.Web.Caching;
    using System.Web.Mvc;
    using Umbraco.Web.Models;
    using Wonderland.Logic.Enums;
    using Wonderland.Logic.Extensions;
    using Wonderland.Logic.Models.Content;
    using Wonderland.Logic.Models.Members;

    public class WonderlandersController : BaseRenderMvcController
    {
        public override ActionResult Index(RenderModel renderModel)
        {
            Wonderlanders model = (Wonderlanders)renderModel.Content;

            Cache cache = System.Web.HttpContext.Current.Cache;

            IEnumerable<PartyHost> topFundraisers = (IEnumerable<PartyHost>)cache["TopFundraisers"];
            IEnumerable<PartyHost> topPartyHosts = (IEnumerable<PartyHost>)cache["TopPartyHosts"];

            if (topFundraisers == null || topPartyHosts == null)
            {
                Random random = new Random(); // used to de-sync the cache expiry times
                random.Next(5);

                if (topFundraisers == null)
                {
                    topFundraisers = this.Members.GetTopPartyHosts(LeaderboardType.TopFundraisers, model.TopFundraisersCount);

                    cache.Insert("TopFundraisers", topFundraisers, null, DateTime.UtcNow.AddMinutes(5 + random.Next(1)), Cache.NoSlidingExpiration);
                }

                if (topPartyHosts == null)
                {
                    topPartyHosts = this.Members.GetTopPartyHosts(LeaderboardType.MostGuests, model.MostGuestsCount);

                    cache.Insert("TopPartyHosts", topPartyHosts, null, DateTime.UtcNow.AddSeconds(5 + random.Next(1)), Cache.NoSlidingExpiration);
                }
            }

            model.TopFundraisers = topFundraisers;
            model.MostGuests = topPartyHosts;

            return this.CurrentTemplate(model);
        }
    }
}
