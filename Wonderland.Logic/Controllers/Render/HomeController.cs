
namespace Wonderland.Logic.Controllers.Render
{
    using System.Web.Mvc;
    using Wonderland.Logic.Models.Content;

    public class HomeController : BaseRenderMvcController
    {
        //[OutputCache(Duration=SitePage.CacheDuration)] removed as this caches the whole page (inc user specific rendering)
        public ActionResult Home() // method executed in preference to the Index, as Home is the template name
        {
            Home model = (Home)this.CurrentPage;

            try
            {
                model.TotalAmountRaised = this.DatabaseContext.Database.ExecuteScalar<decimal>(@"
                                                                                                    SELECT      SUM(Amount)
                                                                                                    FROM        wonderlandDonation
                                                                                                    WHERE       Success = 1
                                                                                                ");
            }
            catch
            {
                model.TotalAmountRaised = 0;
            }

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
