
namespace Wonderland.Logic.Controllers.Surface
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Umbraco.Web.Mvc;
    using Wonderland.Logic.Extensions;
    using Wonderland.Logic.Models.Content;
    using Wonderland.Logic.Models.Database;
    using Wonderland.Logic.Models.Entities;
    using Wonderland.Logic.Models.Forms;
    using Wonderland.Logic.Models.Members;

    public class PartySurfaceController : SurfaceController
    {
        [ChildActionOnly]
        [MemberAuthorize(AllowType = PartyHost.Alias)]
        public ActionResult RenderPartyImageForm()
        {
            PartyImageForm partyImageForm = new PartyImageForm();

            partyImageForm.DefaultImages = ((Party)this.CurrentPage).DefaultImages;

            return this.PartialView("PartyImageForm", partyImageForm);
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

            return Json(formResponse);
        }

        [ChildActionOnly]
        [MemberAuthorize(AllowType = PartyHost.Alias)]
        public ActionResult RenderCustomPartyImageForm()
        {
            return this.PartialView("CustomPartyImageForm", new CustomPartyImageForm());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [MemberAuthorize(AllowType = PartyHost.Alias)]
        public JsonResult HandleCustomPartyImageForm(CustomPartyImageForm customPartyImageForm)
        {
            FormResponse formResponse = new FormResponse();

            if (this.ModelState.IsValid && customPartyImageForm.CustomPartyImage.ContentLength > 0 && customPartyImageForm.CustomPartyImage.InputStream.IsImage())
            {
                string fileName = Guid.NewGuid().ToString() + "." + customPartyImageForm.CustomPartyImage.ContentType.Split('/')[1];

                customPartyImageForm.CustomPartyImage.SaveAs(Server.MapPath("~/Uploads/PartyImage/" + fileName));

                formResponse.Message = "/Uploads/PartyImage/" + fileName;

                formResponse.Success = true;
            }

            return Json(formResponse);
        }

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

            if (this.ModelState.IsValid)
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

            if (this.ModelState.IsValid)
            {                
                if (profileImageForm.ProfileImage.ContentLength > 0 && profileImageForm.ProfileImage.InputStream.IsImage())
                {
                    // WARNING: user may upload an image, but use an incorrect extension !
                    string fileName = Guid.NewGuid().ToString() + "." + profileImageForm.ProfileImage.ContentType.Split('/')[1];

                    profileImageForm.ProfileImage.SaveAs(Server.MapPath("~/Uploads/Profile/" + fileName));

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
            
            return this.PartialView("PartyDetailsForm", partyDetailsForm);
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

                partyHost.PartyHeading = partyDetailsForm.PartyHeading;

                partyHost.PartyDateTime = partyDetailsForm.PartyDateTime;

                Address address = new Address()
                {
                    Address1 = partyDetailsForm.Address1.Replace(Environment.NewLine, string.Empty),
                    Address2 = partyDetailsForm.Address2.Replace(Environment.NewLine, string.Empty),
                    TownCity = partyDetailsForm.TownCity.Replace(Environment.NewLine, string.Empty),
                    Postcode = partyDetailsForm.Postcode.Replace(Environment.NewLine, string.Empty)
                };

                partyHost.PartyAddress = address;

                formResponse.Success = true;
            }
            else
            {
                formResponse.Errors = this.ModelState.GetErrors();
            }

            return Json(formResponse);
        }

        [ChildActionOnly]
        [MemberAuthorize(AllowType = PartyHost.Alias)]
        public ActionResult RenderSuggestedDonationForm()
        {
            SuggestedDonationForm suggestedDonationForm = new SuggestedDonationForm();

            PartyHost partyHost = (PartyHost)this.Members.GetCurrentMember();

            suggestedDonationForm.SuggestedDonation = partyHost.SuggestedDonation;

            return this.PartialView("SuggestedDonationForm", suggestedDonationForm);
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

            return Json(formResponse);
        }

        [ChildActionOnly]
        [MemberAuthorize(AllowType = PartyHost.Alias)]
        public ActionResult RenderFundraisingTargetForm()
        {
            FundraisingTargetForm fundraisingTargetForm = new FundraisingTargetForm();

            PartyHost partyHost = (PartyHost)this.Members.GetCurrentMember();

            fundraisingTargetForm.FundraisingTarget = partyHost.FundraisingTarget;

            return this.PartialView("FundraisingTargetForm", fundraisingTargetForm);
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

            return Json(formResponse);
        }

        [ChildActionOnly]
        [MemberAuthorize]
        public ActionResult RenderPartyWallMessageForm()
        {
            // TODO: safety check that current member is associated with this party ?

            return this.PartialView("PartyWallMessageForm", new PartyWallMessageForm());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [MemberAuthorize]
        public JsonResult HandlePartyWallMessageForm(PartyWallMessageForm partyWallMessageForm)
        {
            // TODO: safety check that current member is associated with this party

            FormResponse formResponse = new FormResponse();

            if (this.ModelState.IsValid && (!string.IsNullOrWhiteSpace(partyWallMessageForm.Message) || !string.IsNullOrWhiteSpace(partyWallMessageForm.PartyWallImage)))
            {
                //Guid partyGuid = ((Party)this.Umbraco.AssignedContentItem).PartyHost.PartyGuid;

                string partyWallImage = partyWallMessageForm.PartyWallImage;

                if (partyWallImage.StartsWith("/Uploads/PartyWall/"))
                {
                    partyWallImage = partyWallImage.Remove(0, "/Uploads/PartyWall/".Length);
                }

                //// insert message into DB
                this.DatabaseContext.Database.Insert(new MessageRow()
                                                            {
                                                                MemberId = this.Members.GetCurrentMemberId(),
                                                                Text = partyWallMessageForm.Message,
                                                                Image = partyWallMessageForm.PartyWallImage
                                                            });

                formResponse.Success = true;
            }

            return Json(formResponse);
        }

        [ChildActionOnly]
        [MemberAuthorize]
        public ActionResult RenderPartyWallImageForm()
        {
            // TODO: safety check that current member is associated with this party ?

            return this.PartialView("PartyWallImageForm", new PartyWallImageForm());
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
                string fileName = Guid.NewGuid().ToString() + "." + partyWallImageForm.PartyWallImage.ContentType.Split('/')[1];

                partyWallImageForm.PartyWallImage.SaveAs(Server.MapPath("~/Uploads/PartyWall/" + fileName));

                formResponse.Message = "/Uploads/PartyWall/" + fileName;

                formResponse.Success = true;
            }

            return Json(formResponse);
        }
    }
}
