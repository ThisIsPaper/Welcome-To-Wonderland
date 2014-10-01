
namespace Wonderland.Logic.Controllers.Surface
{
    using System;
    using System.Web.Mvc;
    using Umbraco.Core.Models;
    using Umbraco.Web.Mvc;
    using Wonderland.Logic.Extensions;
    using Wonderland.Logic.Interfaces;
    using Wonderland.Logic.Models.Entities;
    using Wonderland.Logic.Models.Forms;

    public class ProfileSurfaceController : SurfaceController
    {
        [ChildActionOnly]
        [MemberAuthorize]
        public ActionResult RenderProfileBillingAddressForm()
        {
            ProfileBillingAddressForm profileBillingAddressForm = new ProfileBillingAddressForm();

            IPartier partier = this.Members.GetCurrentPartier();

            profileBillingAddressForm.Address1 = partier.BillingAddress.Address1;
            profileBillingAddressForm.Address2 = partier.BillingAddress.Address2;
            profileBillingAddressForm.TownCity = partier.BillingAddress.TownCity;
            profileBillingAddressForm.Postcode = partier.BillingAddress.Postcode;

            return this.PartialView("ProfileBillingAddressForm", profileBillingAddressForm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [MemberAuthorize]
        public ActionResult HandleProfileBillingAddressForm(ProfileBillingAddressForm profileBillingAddressForm)
        {
            FormResponse formResponse = new FormResponse();

            if (this.ModelState.IsValid)
            {
                Address address = new Address()
                                    {
                                        Address1 = profileBillingAddressForm.Address1,
                                        Address2 = profileBillingAddressForm.Address2,
                                        TownCity = profileBillingAddressForm.TownCity,
                                        Postcode = profileBillingAddressForm.Postcode
                                    };

                IPartier partier = this.Members.GetCurrentPartier();

                partier.BillingAddress = address;

                formResponse.Success = true;
            }
            else
            {
                formResponse.Errors = this.ModelState.GetErrors();
            }

            return Json(formResponse);
        }

        [ChildActionOnly]
        [MemberAuthorize]
        public ActionResult RenderProfileChangePasswordForm()
        {
            return this.PartialView("ProfileChangePasswordForm", new ProfileChangePasswordForm());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [MemberAuthorize]
        public ActionResult HandleProfileChangePasswordForm(ProfileChangePasswordForm profileChangePasswordForm)
        {
            FormResponse formResponse = new FormResponse();

            if (this.ModelState.IsValid)
            {
                IPartier partier = this.Members.GetCurrentPartier();


                // IMPORTANT ! TODO: check existing password


                IMember member = this.Services.MemberService.GetById(partier.Id);
                if (member != null)
                {
                    this.Services.MemberService.SavePassword(member, profileChangePasswordForm.Password);
                }

                formResponse.Success = true;
            }
            else
            {
                formResponse.Errors = this.ModelState.GetErrors();
            }

            return Json(formResponse);
        }


        [ChildActionOnly]
        [MemberAuthorize]
        public ActionResult RenderProfileImageForm()
        {
            return this.PartialView("ProfileImageForm", new ProfileImageForm());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [MemberAuthorize]
        public ActionResult HandleProfileImageForm(ProfileImageForm profileImageForm)
        {
            FormResponse formResponse = new FormResponse();

            if (this.ModelState.IsValid)
            {
                IPartier partier = this.Members.GetCurrentPartier();

                if (profileImageForm.ProfileImage.ContentLength > 0 && profileImageForm.ProfileImage.InputStream.IsImage())
                {
                    // WARNING: user may upload an image, but use an incorrect extension !
                    string fileName = Guid.NewGuid().ToString() + "." + profileImageForm.ProfileImage.ContentType.Split('/')[1];

                    profileImageForm.ProfileImage.SaveAs(Server.MapPath("~/Uploads/Profile/" + fileName));

                    //update property
                    partier.ProfileImage = fileName;

                    // re-inflate the current user model (to take into account newly set property)
                    formResponse.Message = "/Uploads/Profile/" + fileName;

                    formResponse.Success = true;
                }

                formResponse.Success = true;
            }
            else
            {
                formResponse.Errors = this.ModelState.GetErrors();
            }

            return Json(formResponse);
        }

        [ChildActionOnly]
        [MemberAuthorize]
        public ActionResult RenderProfileNamesForm()
        {
            ProfileNamesForm profileNamesForm = new ProfileNamesForm();

            IPartier partier = this.Members.GetCurrentPartier();

            profileNamesForm.FirstName = partier.FirstName;
            profileNamesForm.LastName = partier.LastName;

            return this.PartialView("ProfileNamesForm", profileNamesForm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [MemberAuthorize]
        public ActionResult HandleProfileNamesForm(ProfileNamesForm profileNamesForm)
        {
            FormResponse formResponse = new FormResponse();

            if (this.ModelState.IsValid)
            {
                IPartier partier = this.Members.GetCurrentPartier();

                partier.FirstName = profileNamesForm.FirstName;
                partier.LastName = profileNamesForm.LastName;

                formResponse.Success = true;
            }
            else
            {
                formResponse.Errors = this.ModelState.GetErrors();
            }

            return Json(formResponse);
        }
    }
}
