
namespace Wonderland.Logic.Controllers.Render
{
    using System;
    using System.Web.Mvc;
    using Wonderland.Logic.Enums;
    using Wonderland.Logic.Models.Content;

    //[OutputCache(Duration=60)]
    public class HomeController : BaseRenderMvcController
    {
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
    }
}
