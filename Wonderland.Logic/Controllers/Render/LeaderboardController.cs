
namespace Wonderland.Logic.Controllers.Render
{
    using System;
    using System.Web.Mvc;
    using Wonderland.Logic.Enums;
    using Wonderland.Logic.Models.Content;

    public class LeaderboardController : BaseRenderMvcController
    {
        public ActionResult Leaderboard()
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

            return this.CurrentTemplate(model);
        }
    }
}
