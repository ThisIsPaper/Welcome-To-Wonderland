
namespace Wonderland.Logic.Controllers.Render
{
    using System;
    using System.Web.Caching;
    using System.Web.Mvc;
    using Wonderland.Logic.Models.Content;

    public class HomeController : BaseRenderMvcController
    {
        //[OutputCache(Duration=SitePage.CacheDuration)] removed as this caches the whole page (inc user specific rendering)
        public ActionResult Home() // method executed in preference to the Index, as Home is the template name
        {
            Home model = (Home)this.CurrentPage;
            
            Cache cache = System.Web.HttpContext.Current.Cache;

            decimal? totalAmountRaised = (decimal?)cache["TotalAmountRaised"];

            if (!totalAmountRaised.HasValue)
            {
                try
                {
                    totalAmountRaised = this.DatabaseContext.Database.ExecuteScalar<decimal>(@"
                                                                                                SELECT      SUM(Amount)
                                                                                                FROM        wonderlandDonation
                                                                                                WHERE       Success = 1
                                                                                            ");
                }
                catch
                {
                    totalAmountRaised = 0;
                }

                cache.Insert("TotalAmountRaised", totalAmountRaised); //, null, DateTime.UtcNow.AddMinutes(10), Cache.NoSlidingExpiration);
            }

            model.TotalAmountRaised = totalAmountRaised.Value;

            return this.CurrentTemplate(model);
        }

        // this is hijacking the url /logout,  as it's actually home using the alternate tempalte syntax
        public ActionResult Logout()
        {
            this.Members.Logout();

            return this.Redirect("/");
        }
    }
}
