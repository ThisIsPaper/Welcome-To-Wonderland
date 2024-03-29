﻿
namespace Wonderland.Logic.Controllers.Surface
{
    using Newtonsoft.Json;
    using System;
    using System.Web.Mvc;
    using Umbraco.Core.Models;
    using Umbraco.Web.Mvc;
    using Wonderland.Logic.DotMailer;
    using Wonderland.Logic.Extensions;
    using Wonderland.Logic.Interfaces;
    using Wonderland.Logic.Models.Content;
    using Wonderland.Logic.Models.Entities;
    using Wonderland.Logic.Models.Forms;
    using Wonderland.Logic.Models.Media;
    using Wonderland.Logic.Models.Members;
    using Wonderland.Logic.Models.Messages;

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

            return this.PartialView("Profile/Forms/ProfileBillingAddressForm", profileBillingAddressForm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [MemberAuthorize]
        public ActionResult HandleProfileBillingAddressForm(ProfileBillingAddressForm profileBillingAddressForm)
        {
            FormResponse formResponse = new FormResponse();

            if (this.ModelState.IsValid)
            {
                Address address = new Address(
                                        profileBillingAddressForm.Address1,
                                        profileBillingAddressForm.Address2,
                                        profileBillingAddressForm.TownCity,
                                        profileBillingAddressForm.Postcode);

                IPartier partier = this.Members.GetCurrentPartier();

                partier.BillingAddress = address;

                this.CheckRegistrationComplete(partier);           

                formResponse.Success = true;
            }
            else
            {
                formResponse.Errors = this.ModelState.GetErrors();
            }

            return Json(formResponse, "text/plain");
        }

        [ChildActionOnly]
        [MemberAuthorize]
        public ActionResult RenderProfileChangePasswordForm()
        {
            return this.PartialView("Profile/Forms/ProfileChangePasswordForm", new ProfileChangePasswordForm());
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

            return Json(formResponse, "text/plain");
        }


        [ChildActionOnly]
        [MemberAuthorize]
        public ActionResult RenderProfileImageForm()
        {
            return this.PartialView("Profile/Forms/ProfileImageForm", new ProfileImageForm());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [MemberAuthorize]
        public ActionResult HandleProfileImageForm(ProfileImageForm profileImageForm)
        {
            FormResponse formResponse = new FormResponse();

            if (this.ModelState.IsValid)
            {
                formResponse.Success = true;

                IPartier partier = this.Members.GetCurrentPartier();

                if (profileImageForm.ProfileImage != null && profileImageForm.ProfileImage.ContentLength > 0 && profileImageForm.ProfileImage.InputStream.IsImage())
                {
                    ProfileImage profileImage = partier.ProfileImage;

                    string url = string.Empty;

                    if (profileImage == null)
                    {
                        // create new profile image
                        profileImage = ProfileImages.CreateProfileImage(profileImageForm.ProfileImage);

                        // get the new url
                        url = profileImage.Url;

                        // update pointer on partier
                        partier.ProfileImage = profileImage;
                    }
                    else
                    {
                        // update existing profile image
                        url = profileImage.UploadImage(profileImageForm.ProfileImage);
                    }

                    formResponse.Message = JsonConvert.SerializeObject(new { id = profileImage.Id, url = url }); //TODO:S3URL
                }
                else // remove reference to image
                {
                    // this will cause the file deletion
                    partier.ProfileImage = null;
                }            
            }
            else
            {
                formResponse.Errors = this.ModelState.GetErrors();
            }

            return Json(formResponse, "text/plain");
        }

        [ChildActionOnly]
        [MemberAuthorize]
        public ActionResult RenderProfileNamesForm()
        {
            ProfileNamesForm profileNamesForm = new ProfileNamesForm();

            IPartier partier = this.Members.GetCurrentPartier();

            profileNamesForm.FirstName = partier.FirstName;
            profileNamesForm.LastName = partier.LastName;

            return this.PartialView("Profile/Forms/ProfileNamesForm", profileNamesForm);
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

                this.CheckRegistrationComplete(partier);           

                formResponse.Success = true;
            }
            else
            {
                formResponse.Errors = this.ModelState.GetErrors();
            }

            return Json(formResponse, "text/plain");
        }

        [ChildActionOnly]
        [MemberAuthorize(AllowType=PartyHost.Alias)]
        public ActionResult RenderProfileTShirtSizeForm()
        {
            ProfileTShirtSizeForm profileTShirtSizeForm = new ProfileTShirtSizeForm();

            RegisterHost registerHost = (RegisterHost)this.Umbraco.TypedContentSingleAtXPath("//" + RegisterHost.Alias);

            this.ViewBag.TShirtSizes = registerHost.TShirtSizes;

            PartyHost partyHost = (PartyHost)this.Members.GetCurrentPartier();

            profileTShirtSizeForm.TShirtSize = partyHost.TShirtSize;

            return this.PartialView("Profile/Forms/ProfileTShirtSizeForm", profileTShirtSizeForm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [MemberAuthorize(AllowType=PartyHost.Alias)]
        public ActionResult HandleProfileTShirtSizeForm(ProfileTShirtSizeForm profileTShirtSizeForm)
        {
            FormResponse formResponse = new FormResponse();

            if (this.ModelState.IsValid)
            {
                PartyHost partyHost = (PartyHost)this.Members.GetCurrentPartier();

                partyHost.TShirtSize = profileTShirtSizeForm.TShirtSize;

                formResponse.Success = true;
            }
            else
            {
                formResponse.Errors = this.ModelState.GetErrors();
            }

            return Json(formResponse, "text/plain");
        }

        /// <summary>
        /// a member can register, and skip the 2nd step
        /// if these values are then set afterwards when editing this profile, then we can then mark the registration as complete
        /// </summary>
        /// <param name="partier"></param>
        private void CheckRegistrationComplete(IPartier partier)
        {
            // once it has been completed, we never revert back, so only process if dot mailer thinks the host hasn't yet completed registration
            if (!partier.DotMailerRegistrationComplete)
            {
                if (!string.IsNullOrWhiteSpace(partier.FirstName) && !string.IsNullOrWhiteSpace(partier.LastName) && !string.IsNullOrWhiteSpace(partier.BillingAddress.ToString()))
                {
                    if (partier is PartyHost)
                    {
                        DotMailerService.HostRegistrationCompleted((Contact)(PartyHost)partier);

                    }
                    else if (partier is PartyGuest)
                    {
                        DotMailerService.GuestRegistrationCompleted((Contact)(PartyGuest)partier);
                    }
                }
            }
        }
    }
}
