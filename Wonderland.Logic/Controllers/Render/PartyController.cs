
namespace Wonderland.Logic.Controllers.Render
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Umbraco.Core.Models;
    using Umbraco.Web.Models;
    using Wonderland.Logic.Models.Content;
    using Wonderland.Logic.Models.Members;

    public class PartyController : BaseRenderMvcController
    {
        public override ActionResult Index(RenderModel renderModel) // have to override index instead of a Party() method, as there isn't a Party template
        {
            Party model = (Party)renderModel.Content;

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
                if (this.Members.IsLoggedIn())
                {                    
                    IPublishedContent currentMember = this.Members.GetCurrentMember();

                    if (currentMember is PartyHost)
                    {
                        return this.Redirect(model.Url + ((PartyHost)currentMember).PartyUrlIdentifier + "/");
                    }
                    else if (currentMember is PartyGuest)
                    {
                        // TODO:
                    }
                }

                // fallback
                return this.Redirect(Home.GetCurrentHome(model).Url);
            }

            // known host, so build the renderModel renderModel
            model.PartyHost = partyHost;

            // image

            // location

            // heading
            if (!string.IsNullOrWhiteSpace(partyHost.PartyHeading))
            {
                model.Heading = partyHost.PartyHeading;
            }
            
            // copy
            if (!string.IsNullOrWhiteSpace(partyHost.PartyCopy))
            {
                model.Copy = partyHost.PartyCopy;
            }

            // wall

            // totaliser

            // partiers - the host + all guests
            List<IPartier> partiers = new List<IPartier>();
            partiers.Add(partyHost);
           

            model.Partiers = partiers;

            // badges


            // detemine view to return based on the user
            if (this.Members.IsLoggedIn())
            {                
                if (((IPartier)this.Members.GetCurrentMember()).Id == partyHost.Id)
                {
                    return View("Host", model);
                }

                // TODO: check to see if current user is a guest at this renderModel
            }

            // fallback
            return View("Anonymous", model);
        }
    }
}
