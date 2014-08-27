
namespace Wonderland.Logic.Controllers
{
    using System;
    using System.Web.Mvc;
    using Umbraco.Web.Mvc;
    using Wonderland.Logic.Enums;
    using Wonderland.Logic.Models.Content;

    public class LeaderboardController : RenderMvcController
    {
        public ActionResult Leaderboard()
        {
            Leaderboard model = CurrentPage as Leaderboard;

            try
            {
                model.LeaderboardType = (LeaderboardType)Enum.Parse(typeof(LeaderboardType), this.Request.QueryString.ToString(), true);
            }
            catch
            {
                return this.Redirect(model.Parent.Url);
            }                

            return this.CurrentTemplate(model);
        }
    }
}
