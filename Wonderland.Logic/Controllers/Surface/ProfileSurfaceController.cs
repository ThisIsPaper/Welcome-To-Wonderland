
namespace Wonderland.Logic.Controllers.Surface
{
    using System;
    using System.Web.Mvc;
    using Umbraco.Web.Mvc;
    using Wonderland.Logic.Interfaces;
    using Wonderland.Logic.Models.Content;
    using Wonderland.Logic.Models.Forms;
    using Wonderland.Logic.Extensions;
    using Umbraco.Core.Models;
    using Wonderland.Logic.Models.Entities;

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

                // TODO:

                formResponse.Success = true;
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

                // TODO:

                formResponse.Success = true;
            }

            return Json(formResponse);
        }

        [ChildActionOnly]
        [MemberAuthorize]
        public ActionResult RenderProfileNamesForm()
        {
            return this.PartialView("ProfileNamesForm", new ProfileNamesForm());
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

                // TODO:

                formResponse.Success = true;
            }

            return Json(formResponse);
        }
    }
}
