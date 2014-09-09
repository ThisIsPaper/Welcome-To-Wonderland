﻿
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
                    return this.Redirect(((IPartier)this.Members.GetCurrentMember()).GetPartyUrl());
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
                IPartier partier = (IPartier)this.Members.GetCurrentMember();
                if (partier is PartyHost && partier.Id == partyHost.Id)
                {
                    return View("Host", model);
                }
                else if (partier is PartyGuest && ((PartyGuest)partier).PartyGuid == partyHost.PartyGuid)
                {
                    return View("Guest", model);
                }

                //if (((IPartier)this.Members.GetCurrentMember()).Id == partyHost.Id)
                //{
                //    return View("Host", model);
                //}

                //// TODO: check to see if current user is a guest at this renderModel
                //if (((IPartier)this.Members.GetCurrentMember()).PartyGuid == partyHost.PartyGuid)
                //{

                //}
            }

            // fallback
            return View("Anonymous", model);
        }
    }
}
