﻿
namespace Wonderland.Logic.Controllers.Surface
{
    using System;
    using System.Web.Mvc;
    using Umbraco.Web.Mvc;
    using Wonderland.Logic.Models.Database;
    using Wonderland.Logic.Models.Forms;
    using Wonderland.Logic.Models.Members;
    using Wonderland.Logic.Models.Content;
    using System.IO;
    using System.Drawing;
    using System.Drawing.Imaging;
    using Wonderland.Logic.Extensions;
    using Wonderland.Logic.Models.Entities;

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
        //[ValidateAntiForgeryToken]
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
            partyDetailsForm.PartyDate = partyHost.PartyDateTime;
            partyDetailsForm.PartyTime = partyHost.PartyDateTime;

            partyDetailsForm.Address1 = partyHost.PartyAddress.Address1;
            partyDetailsForm.Address2 = partyHost.PartyAddress.Address2;
            partyDetailsForm.TownCity = partyHost.PartyAddress.TownCity;
            partyDetailsForm.Postcode = partyHost.PartyAddress.Postcode;
            
            return this.PartialView("PartyDetailsForm", partyDetailsForm);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        [MemberAuthorize(AllowType = PartyHost.Alias)]
        public JsonResult HandlePartyDetailsForm(PartyDetailsForm partyDetailsForm)
        {
            FormResponse formResponse = new FormResponse();

            if (!this.ModelState.IsValid)
            {
                formResponse.Success = false;
            }
            else
            {
                PartyHost partyHost = (PartyHost)this.Members.GetCurrentMember();

                partyHost.PartyHeading = partyDetailsForm.PartyHeading;

                partyHost.PartyDateTime = partyDetailsForm.PartyDate.Add(partyDetailsForm.PartyTime.TimeOfDay);

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
        //[ValidateAntiForgeryToken]
        [MemberAuthorize(AllowType = PartyHost.Alias)]
        public JsonResult HandleSuggestedDonationForm(SuggestedDonationForm suggestedDonationForm)
        {
            FormResponse formResponse = new FormResponse();

            if (!this.ModelState.IsValid)
            {
                formResponse.Success = false;
            }
            else
            {
                ((PartyHost)this.Members.GetCurrentMember()).SuggestedDonation = suggestedDonationForm.SuggestedDonation;

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
        //[ValidateAntiForgeryToken]
        [MemberAuthorize]
        public JsonResult HandlePartyWallMessageForm(PartyWallMessageForm partyWallMessageForm)
        {
            // TODO: safety check that current member is associated with this party

            FormResponse formResponse = new FormResponse();

            if (this.ModelState.IsValid && (!string.IsNullOrWhiteSpace(partyWallMessageForm.Message) || !string.IsNullOrWhiteSpace(partyWallMessageForm.PartyWallImage)))
            {
                //Guid partyGuid = ((Party)this.Umbraco.AssignedContentItem).PartyHost.PartyGuid;

                //// insert message into DB
                //this.DatabaseContext.Database.Insert(new Message()
                //                                            {
                //                                                MemberId = this.Members.GetCurrentMemberId(),
                //                                                Text = partyWallMessageForm.Message,
                //                                                Image = partyWallMessageForm.PartyWallImage
                //                                            });

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
        //[ValidateAntiForgeryToken]
        [MemberAuthorize]
        public JsonResult HandlePartyWallImageForm(PartyWallImageForm partyWallImageForm)
        {
            FormResponse formResponse = new FormResponse();

            // TODO: safety check that current member is associated with this party

            if (this.ModelState.IsValid && partyWallImageForm.PartyWallImage.ContentLength > 0 && partyWallImageForm.PartyWallImage.InputStream.IsImage())
            {
                string fileName = Guid.NewGuid().ToString() + "." + partyWallImageForm.PartyWallImage.ContentType.Split('/')[1];

                partyWallImageForm.PartyWallImage.SaveAs(Server.MapPath("~/Uploads/PartyWall/" + fileName));

                formResponse.Message = fileName;

                formResponse.Success = true;
            }

            return Json(formResponse);
        }
    }
}
