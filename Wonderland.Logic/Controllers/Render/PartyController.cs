
namespace Wonderland.Logic.Controllers.Render
{
    using System.Linq;
    using System;
    using System.Web.Mvc;
    using Wonderland.Logic.Enums;
    using Wonderland.Logic.Models.Content;
    using Wonderland.Logic.Models.Members;

    public class PartyController : BaseRenderMvcController
    {
        //public override ActionResult Index(Umbraco.Web.Models.RenderModel model)
        public ActionResult Party()
        {
            Party model = (Party)this.CurrentPage;

            PartyHost partyHost;

            try
            {
                partyHost = (PartyHost)this.Members.GetById(int.Parse(this.HttpContext.Items["partyHostId"].ToString()));
            }
            catch
            {
                partyHost = null;
            }

            if (!(partyHost is PartyHost))
            {
                // redirect to home, as no known party host
                return this.Redirect(Home.GetCurrentHome(model).Url);
            }

            // associate the party host
            model.PartyHost = partyHost;

            return this.CurrentTemplate(model);
        }
    }
}
