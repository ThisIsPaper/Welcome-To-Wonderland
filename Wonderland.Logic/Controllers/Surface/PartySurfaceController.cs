
namespace Wonderland.Logic.Controllers.Surface
{
    using System;
    using System.Web.Mvc;
    using Umbraco.Web.Mvc;
    using Wonderland.Logic.Models.Forms;
    using Wonderland.Logic.Models.Members;
    using Wonderland.Logic.Models.Content;
    using System.IO;
    using System.Drawing;
    using System.Drawing.Imaging;
    using Wonderland.Logic.Extensions;

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

        [ChildActionOnly]
        [MemberAuthorize(AllowType = PartyHost.Alias)]
        public ActionResult RenderProfileImageForm()
        {
            return this.PartialView("ProfileImageForm", new ProfileImageForm());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [MemberAuthorize(AllowType = PartyHost.Alias)]
        public JsonResult HandleProfileImageForm(ProfileImageForm profileImageForm)
        {
            FormResponse formResponse = new FormResponse();

            if (!this.ModelState.IsValid)
            {
                formResponse.Success = false;
            }
            else
            {                
                if (profileImageForm.ProfileImage.ContentLength > 0 && profileImageForm.ProfileImage.InputStream.IsImage())
                {
                    // WARNING: user may upload an image, but use an incorrect extension !
                    string fileName = Guid.NewGuid().ToString() + "." + profileImageForm.ProfileImage.ContentType.Split('/')[1];

                    profileImageForm.ProfileImage.SaveAs(Server.MapPath("~/Uploads/ProfileImages/" + fileName));

                    //update PartyHost property
                    PartyHost partyHost = (PartyHost)this.Members.GetCurrentMember();
                    partyHost.ProfileImage = fileName;

                    // re-inflate the current user model (to take into account newly set property)
                    formResponse.Message = new PartyHost(this.Umbraco.TypedMember(partyHost.Id)).ProfileImageUrl;
                    formResponse.Success = true;
                }
            }

            return Json(formResponse);
        }
    }
}
