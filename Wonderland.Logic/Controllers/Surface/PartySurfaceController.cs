
namespace Wonderland.Logic.Controllers.Surface
{
    using System;
    using System.Web.Mvc;
    using Umbraco.Web.Mvc;
    using Wonderland.Logic.DotMailer;
    using Wonderland.Logic.Extensions;
    using Wonderland.Logic.Models.Content;
    using Wonderland.Logic.Models.Database;
    using Wonderland.Logic.Models.Entities;
    using Wonderland.Logic.Models.Forms;
    using Wonderland.Logic.Models.Media;
    using Wonderland.Logic.Models.Members;
    using Wonderland.Logic.Models.Messages;

    public class PartySurfaceController : SurfaceController
    {
        [ChildActionOnly]
        [MemberAuthorize(AllowType = PartyHost.Alias)]
        public ActionResult RenderPartyImageForm()
        {
            PartyImageForm partyImageForm = new PartyImageForm();

            partyImageForm.DefaultImages = ((Party)this.CurrentPage).DefaultImages;

            return this.PartialView("Party/Forms/PartyImageForm", partyImageForm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [MemberAuthorize(AllowType = PartyHost.Alias)]
        public JsonResult HandlePartyImageForm(PartyImageForm partyImageForm)
        {
            FormResponse formResponse = new FormResponse();

            if (this.ModelState.IsValid)
            {
                ((PartyHost)this.Members.GetCurrentMember()).PartyImage = partyImageForm.PartyImage;

                formResponse.Success = true;
            }
            else
            {
                formResponse.Errors = this.ModelState.GetErrors();
            }

            return Json(formResponse, "text/plain");
        }

        [ChildActionOnly]
        [MemberAuthorize(AllowType = PartyHost.Alias)]
        public ActionResult RenderCustomPartyImageForm()
        {
            return this.PartialView("Party/Forms/CustomPartyImageForm", new CustomPartyImageForm());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [MemberAuthorize(AllowType = PartyHost.Alias)]
        public JsonResult HandleCustomPartyImageForm(CustomPartyImageForm customPartyImageForm)
        {
            FormResponse formResponse = new FormResponse();

            if (this.ModelState.IsValid && customPartyImageForm.CustomPartyImage.ContentLength > 0 && customPartyImageForm.CustomPartyImage.InputStream.IsImage())
            {
                // NOTE: should update to return cms image ID in addition to the image url

                int id = PartyImages.CreatePartyImage(customPartyImageForm.CustomPartyImage);

                formResponse.Message = this.Umbraco.TypedMedia(id).Url;

                formResponse.Success = true;
            }

            return Json(formResponse, "text/plain");
        }

        [ChildActionOnly]
        [MemberAuthorize(AllowType=PartyHost.Alias)]
        public ActionResult RenderPartyCopyForm()
        {
            PartyCopyForm partyCopyForm = new PartyCopyForm();

            partyCopyForm.Copy = ((PartyHost)this.Members.GetCurrentMember()).PartyCopy;

            return this.PartialView("Party/Forms/PartyCopyForm", partyCopyForm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [MemberAuthorize(AllowType=PartyHost.Alias)]
        public JsonResult HandlePartyCopyForm(PartyCopyForm partyCopyForm)
        {
            FormResponse formResponse = new FormResponse();

            if (this.ModelState.IsValid)
            {
                // change copy for current party host
                PartyHost partyHost = (PartyHost)this.Members.GetCurrentMember();

                partyHost.PartyCopy = partyCopyForm.Copy;

                formResponse.Success = true;
            }
            else
            {
                formResponse.Errors = this.ModelState.GetErrors();
            }

            return Json(formResponse, "text/plain");
        }

        [ChildActionOnly]
        [MemberAuthorize(AllowType = PartyHost.Alias)]
        public ActionResult RenderProfileImageForm()
        {
            return this.PartialView("Party/Forms/ProfileImageForm", new ProfileImageForm());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [MemberAuthorize(AllowType = PartyHost.Alias)]
        public JsonResult HandleProfileImageForm(ProfileImageForm profileImageForm)
        {
            FormResponse formResponse = new FormResponse();

            if (this.ModelState.IsValid)
            {
                //update PartyHost property
                PartyHost partyHost = (PartyHost)this.Members.GetCurrentMember();

                if (profileImageForm.ProfileImage != null && profileImageForm.ProfileImage.ContentLength > 0 && profileImageForm.ProfileImage.InputStream.IsImage())
                {
                    int id = ProfileImages.CreateProfileImage(profileImageForm.ProfileImage);

                    string url = this.Umbraco.TypedMedia(id).Url;
                    
                    // TODO: change this to id ?
                    partyHost.ProfileImage = url;

                    // re-inflate the current user model (to take into account newly set property)
                    formResponse.Message = url; //new PartyHost(this.Umbraco.TypedMember(partyHost.Id)).ProfileImageUrl;
                }
                else
                {
                    partyHost.ProfileImage = string.Empty;
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
        [MemberAuthorize(AllowType = PartyHost.Alias)]
        public ActionResult RenderPartyDetailsForm()
        {
            PartyDetailsForm partyDetailsForm = new PartyDetailsForm();

            PartyHost partyHost = (PartyHost)this.Members.GetCurrentMember();

            partyDetailsForm.PartyHeading = partyHost.PartyHeading;
            partyDetailsForm.PartyDateTime = partyHost.PartyDateTime;

            partyDetailsForm.Address1 = partyHost.PartyAddress.Address1;
            partyDetailsForm.Address2 = partyHost.PartyAddress.Address2;
            partyDetailsForm.TownCity = partyHost.PartyAddress.TownCity;
            partyDetailsForm.Postcode = partyHost.PartyAddress.Postcode;

            return this.PartialView("Party/Forms/PartyDetailsForm", partyDetailsForm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [MemberAuthorize(AllowType = PartyHost.Alias)]
        public JsonResult HandlePartyDetailsForm(PartyDetailsForm partyDetailsForm)
        {
            FormResponse formResponse = new FormResponse();

            if (this.ModelState.IsValid)
            {
                PartyHost partyHost = (PartyHost)this.Members.GetCurrentMember();

                bool updateDotMailer = false;

                if (partyHost.PartyHeading != partyDetailsForm.PartyHeading)
                {
                    partyHost.PartyHeading = partyDetailsForm.PartyHeading;
                }

                if (partyHost.PartyDateTime != partyDetailsForm.PartyDateTime)
                {
                    partyHost.PartyDateTime = partyDetailsForm.PartyDateTime;

                    updateDotMailer = true;
                }

                Address address = new Address(
                                            partyDetailsForm.Address1,
                                            partyDetailsForm.Address2,
                                            partyDetailsForm.TownCity,
                                            partyDetailsForm.Postcode);

                if (partyHost.PartyAddress.ToString() != address.ToString())
                {
                    partyHost.PartyAddress = address;

                    updateDotMailer = true;
                }

                if (updateDotMailer)
                {
                    DotMailerService.UpdatePartyDetails(partyHost);
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
        [MemberAuthorize(AllowType = PartyHost.Alias)]
        public ActionResult RenderSuggestedDonationForm()
        {
            SuggestedDonationForm suggestedDonationForm = new SuggestedDonationForm();

            PartyHost partyHost = (PartyHost)this.Members.GetCurrentMember();

            suggestedDonationForm.SuggestedDonation = partyHost.SuggestedDonation;

            return this.PartialView("Party/Forms/SuggestedDonationForm", suggestedDonationForm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [MemberAuthorize(AllowType = PartyHost.Alias)]
        public JsonResult HandleSuggestedDonationForm(SuggestedDonationForm suggestedDonationForm)
        {
            FormResponse formResponse = new FormResponse();

            if (this.ModelState.IsValid)
            {
                ((PartyHost)this.Members.GetCurrentMember()).SuggestedDonation = suggestedDonationForm.SuggestedDonation;

                formResponse.Success = true;
            }
            else
            {
                formResponse.Errors = this.ModelState.GetErrors();
            }

            return Json(formResponse, "text/plain");
        }

        [ChildActionOnly]
        [MemberAuthorize(AllowType = PartyHost.Alias)]
        public ActionResult RenderFundraisingTargetForm()
        {
            FundraisingTargetForm fundraisingTargetForm = new FundraisingTargetForm();

            PartyHost partyHost = (PartyHost)this.Members.GetCurrentMember();

            fundraisingTargetForm.FundraisingTarget = partyHost.FundraisingTarget;

            return this.PartialView("Party/Forms/FundraisingTargetForm", fundraisingTargetForm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [MemberAuthorize(AllowType = PartyHost.Alias)]
        public JsonResult HandleFundraisingTargetForm(FundraisingTargetForm fundraisingTargetForm)
        {
            FormResponse formResponse = new FormResponse();

            if (this.ModelState.IsValid)
            {
                ((PartyHost)this.Members.GetCurrentMember()).FundraisingTarget = fundraisingTargetForm.FundraisingTarget;

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
        public ActionResult RenderPartyWallMessageForm()
        {
            // TODO: safety check that current member is associated with this party ?

            return this.PartialView("Party/Forms/PartyWallMessageForm", new PartyWallMessageForm());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [MemberAuthorize]
        public JsonResult HandlePartyWallMessageForm(PartyWallMessageForm partyWallMessageForm)
        {
            // TODO: safety check that current member is associated with this party
            //Party party = (Party)this.Umbraco.AssignedContentItem;;

            FormResponse formResponse = new FormResponse();

            if (this.ModelState.IsValid && (!string.IsNullOrWhiteSpace(partyWallMessageForm.Message) || !string.IsNullOrWhiteSpace(partyWallMessageForm.PartyWallImage)))
            {
                //Guid partyGuid = ((Party)this.Umbraco.AssignedContentItem).PartyHost.PartyGuid;

                string partyWallImage = partyWallMessageForm.PartyWallImage;

                //// insert message into DB
                this.DatabaseContext.Database.Insert(new MessageRow()
                                                            {
                                                                MemberId = this.Members.GetCurrentMemberId(),
                                                                Text = partyWallMessageForm.Message,
                                                                Image = partyWallMessageForm.PartyWallImage
                                                            });

                formResponse.Success = true;
            }

            return Json(formResponse, "text/plain");
        }

        [ChildActionOnly]
        [MemberAuthorize]
        public ActionResult RenderPartyWallImageForm()
        {
            // TODO: safety check that current member is associated with this party ?

            return this.PartialView("Party/Forms/PartyWallImageForm", new PartyWallImageForm());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [MemberAuthorize]
        public JsonResult HandlePartyWallImageForm(PartyWallImageForm partyWallImageForm)
        {
            FormResponse formResponse = new FormResponse();

            // TODO: safety check that current member is associated with this party

            if (this.ModelState.IsValid && partyWallImageForm.PartyWallImage.ContentLength > 0 && partyWallImageForm.PartyWallImage.InputStream.IsImage())
            {
                int id = PartyWallImages.CreatePartyWallImage(partyWallImageForm.PartyWallImage);

                string url = this.Umbraco.TypedMedia(id).Url;

                formResponse.Message = url;

                formResponse.Success = true;
            }
            else
            {
                formResponse.Errors = this.ModelState.GetErrors();
            }

            return Json(formResponse, "text/plain");
        }

        private void CheckPartyPageComplete(PartyHost partyHost)
        {
            if (!partyHost.DotMailerPartyPageComplete)
            {
                if (!string.IsNullOrWhiteSpace(partyHost.PartyImage)
                    && partyHost.FundraisingTarget > 0
                    && !string.IsNullOrWhiteSpace(partyHost.PartyAddress.ToString()))
                {
                    partyHost.DotMailerPartyPageComplete = true;

                    // update the host to indicate that their party page is now complete
                    DotMailerService.UpdateContact((Contact)partyHost);
                }
            }
        }
    }
}
