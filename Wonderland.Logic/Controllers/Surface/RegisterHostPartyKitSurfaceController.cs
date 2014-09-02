
namespace Wonderland.Logic.Controllers.Surface
{
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
        public ActionResult Index()
        {
            return this.PartialView("RegisterHostPartyKitFormPartial", new RegisterHostPartyKitForm());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [MemberAuthorize(AllowType=PartyHost.Alias)]
        public ActionResult RegisterHostPartyKitForm(RegisterHostPartyKitForm registerHostPartyKitForm)
        {
            if (!ModelState.IsValid)
            {
                return this.CurrentUmbracoPage();
            }

            PartyHost partyHost = (PartyHost)this.Members.GetCurrentMember();

            partyHost.FirstName = registerHostPartyKitForm.FirstName;
            partyHost.LastName = registerHostPartyKitForm.LastName;

            partyHost.PartyKitAddress = new Address()
                                        {
                                            Address1 = registerHostPartyKitForm.Address1,
                                            Address2 = registerHostPartyKitForm.Address2,
                                            TownCity = registerHostPartyKitForm.TownCity,
                                            Postcode = registerHostPartyKitForm.PostCode
                                        };

            partyHost.HasRequestedPartyKit = true;

            return this.RedirectToUmbracoPage(this.CurrentPage.Parent.Children.Single(x => x.DocumentTypeAlias == RegisterHostPartyUrl.Alias));
        }
    }
}
