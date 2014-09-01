
namespace Wonderland.Logic.Controllers.Surface
{
    using System.Linq;
    using System.Web.Mvc;
    using Umbraco.Web.Mvc;
    using Wonderland.Logic.Models.Content;
    using Wonderland.Logic.Models.Forms;
    using Wonderland.Logic.Models.Members;
    using Wonderland.Logic.Models.Entities;

    public class RegisterHostPartyKitSurfaceController : SurfaceController
    {
        [ChildActionOnly]
        [Authorize(Roles = Partier.HostRoleAlias)]
        public ActionResult Index()
        {
            return this.PartialView("RegisterHostPartyKitFormPartial", new RegisterHostPartyKitForm());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Partier.HostRoleAlias)]
        public ActionResult RegisterHostPartyKitForm(RegisterHostPartyKitForm registerHostPartyKitForm)
        {
            if (!ModelState.IsValid)
            {
                return this.CurrentUmbracoPage();
            }
            
            Partier partier = Partier.GetCurrentPartier(); // no need to null check as only logged in members can get here
            
            partier.FirstName = registerHostPartyKitForm.FirstName;
            partier.LastName = registerHostPartyKitForm.LastName;

            partier.PartyKitAddress = new Address()
                                        {
                                            Address1 = registerHostPartyKitForm.Address1,
                                            Address2 = registerHostPartyKitForm.Address2,
                                            TownCity = registerHostPartyKitForm.TownCity,
                                            Postcode = registerHostPartyKitForm.PostCode
                                        };

            partier.HasRequestedPartyKit = true;

            return this.RedirectToUmbracoPage(this.CurrentPage.Parent.Children.Single(x => x.DocumentTypeAlias == RegisterHostPartyUrl.Alias));
        }
    }
}
