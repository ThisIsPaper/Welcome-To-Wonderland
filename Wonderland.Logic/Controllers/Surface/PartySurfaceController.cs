
namespace Wonderland.Logic.Controllers.Surface
{
    using System.Web.Mvc;
    using Umbraco.Web.Mvc;
    using Wonderland.Logic.Models.Forms;
    using Wonderland.Logic.Models.Members;
    using Wonderland.Logic.Models.Content;

    public class PartySurfaceController : SurfaceController
    {
        [ChildActionOnly]
        [MemberAuthorize(AllowType=PartyHost.Alias)]
        public ActionResult RenderPartyCopyForm()
        {
            PartyCopyForm partyCopyForm = new PartyCopyForm();

            partyCopyForm.Copy = ((PartyHost)this.Members.GetCurrentMember()).PartyCopy;

            return this.PartialView("PartyCopyForm", partyCopyForm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [MemberAuthorize(AllowType=PartyHost.Alias)]
        public JsonResult HandlePartyCopyForm(PartyCopyForm partyCopyForm)
        {
            FormResponse formResponse = new FormResponse();

            if (!this.ModelState.IsValid)
            {
                formResponse.Success = false;
            }
            else
            {
                // change copy for current party host

                PartyHost partyHost = (PartyHost)this.Members.GetCurrentMember();

                partyHost.PartyCopy = partyCopyForm.Copy;

                formResponse.Success = true;
            }

            return Json(formResponse);
        }
    }
}
