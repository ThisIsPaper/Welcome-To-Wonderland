
namespace Wonderland.Logic.Controllers.Render
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Wonderland.Logic.Enums;
    using Wonderland.Logic.Models.Content;
    using Wonderland.Logic.Models.Members;
    using Wonderland.Logic.Extensions;

    public class LeaderboardController : BaseRenderMvcController
    {
        //[OutputCache(Duration=60, VaryByParam="")]
        public ActionResult Leaderboard() // method executed in preference to the Index, as Leaderboard is the template name
        {
            Leaderboard model = (Leaderboard)this.CurrentPage;

            try
            {
                model.LeaderboardType = (LeaderboardType)Enum.Parse(typeof(LeaderboardType), this.Request.QueryString.ToString(), true);
            }
            catch
            {
                return this.Redirect(model.Parent.Url);
            }                
            
            model.TopPartyHosts = this.Members.GetTopPartyHosts(model.LeaderboardType, model.LeaderboardCount);

            return this.CurrentTemplate(model);
        }
    }
}
