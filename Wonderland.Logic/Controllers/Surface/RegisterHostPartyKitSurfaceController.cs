
namespace Wonderland.Logic.Controllers.Surface
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Umbraco.Web.Mvc;
    using Wonderland.Logic.Models.Content;
    using Wonderland.Logic.Models.Entities;
    using Wonderland.Logic.Models.Forms;
    using Wonderland.Logic.Models.Members;

    public class RegisterHostPartyKitSurfaceController : SurfaceController
    {
        [ChildActionOnly]
        [MemberAuthorize(AllowType=PartyHost.Alias)]
        public ActionResult RenderRegisterHostPartyKitForm()
        {
            return this.PartialView("RegisterHostPartyKitForm", new RegisterHostPartyKitForm());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [MemberAuthorize(AllowType=PartyHost.Alias)]
        public ActionResult HandleRegisterHostPartyKitForm(RegisterHostPartyKitForm registerHostPartyKitForm)
        {
            if (!ModelState.IsValid)
            {
                return this.CurrentUmbracoPage();
            }

            PartyHost partyHost = (PartyHost)this.Members.GetCurrentMember();

            partyHost.FirstName = registerHostPartyKitForm.FirstName;
            partyHost.LastName = registerHostPartyKitForm.LastName;

            Address address = new Address()
                                    {
                                        Address1 = registerHostPartyKitForm.Address1.Replace(Environment.NewLine, string.Empty),
                                        Address2 = registerHostPartyKitForm.Address2.Replace(Environment.NewLine, string.Empty),
                                        TownCity = registerHostPartyKitForm.TownCity.Replace(Environment.NewLine, string.Empty),
                                        Postcode = registerHostPartyKitForm.PostCode.Replace(Environment.NewLine, string.Empty)
                                    };

            partyHost.PartyKitAddress = address;
            partyHost.PartyAddress = address;

            partyHost.HasRequestedPartyKit = true;

            return this.RedirectToUmbracoPage(this.CurrentPage.Parent.Children.Single(x => x.DocumentTypeAlias == RegisterHostPartyUrl.Alias));
        }
    }
}
