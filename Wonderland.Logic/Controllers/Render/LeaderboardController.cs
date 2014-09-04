
namespace Wonderland.Logic.Controllers.Render
{
    using System;
    using System.Web.Mvc;
    using Wonderland.Logic.Enums;
    using Wonderland.Logic.Models.Content;

    public class LeaderboardController : BaseRenderMvcController
    {
        public ActionResult Leaderboard() // method executed in preference to the Index, as Leaderboard is the template name
        {
            Leaderboard leaderboard = (Leaderboard)this.CurrentPage;

            try
            {
                leaderboard.LeaderboardType = (LeaderboardType)Enum.Parse(typeof(LeaderboardType), this.Request.QueryString.ToString(), true);
            }
            catch
            {
                return this.Redirect(leaderboard.Parent.Url);
            }                

            return this.CurrentTemplate(leaderboard);
        }
    }
}
