
namespace Wonderland.Logic.Controllers.Surface
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Security;
    using Umbraco.Core;
    using Umbraco.Core.Models;
    using Umbraco.Core.Security;
    using Umbraco.Web.Mvc;
    using Wonderland.Logic.Models.Content;
    using Wonderland.Logic.Models.Forms;
    using Wonderland.Logic.Models.Members;

    public class RegisterHostSurfaceController : SurfaceController
    {
        /// <summary>
        /// returns the partial for the host registration form
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly] // (Cannot redirect from a Child Action - see Controllers\Render\RegisterHostController)
        public ActionResult Index()
        {
            // get the model for the current page
            RegisterHost registerHost = (RegisterHost)this.CurrentPage;

            // get marketing sources for a dropdown 
            List<string> marketingSources = new List<string>(registerHost.MarketingSources);

            // add an empty option the the beginning
            marketingSources.Insert(0, string.Empty);

            // add a dropdown to the viewbag
            this.ViewBag.MarketingSources = new SelectList(marketingSources);

            return this.PartialView("RegisterHostFormPartial", new RegisterHostForm());
        }

        //// commented out until js client side validation wired-up
        //public JsonResult ValidateIsEmailAvailable(string emailAddress)
        //{
        //    return Json(this.Members.GetByUsername(emailAddress) == null, JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterHostForm(RegisterHostForm registerHostForm)
        {
            if (!this.ModelState.IsValid)
            {
                return this.CurrentUmbracoPage();
            }

            UmbracoMembershipProviderBase membersUmbracoMembershipProvider = Membership.Providers[Constants.Conventions.Member.UmbracoMemberProviderName] as UmbracoMembershipProviderBase;

            MembershipCreateStatus membershipCreateStatus;

            MembershipUser membershipUser = membersUmbracoMembershipProvider.CreateUser(
                                                Partier.Alias,
                                                registerHostForm.EmailAddress,
                                                registerHostForm.Password,
                                                registerHostForm.EmailAddress,
                                                null,
                                                null,
                                                true, 
                                                null, 
                                                out membershipCreateStatus);
            
            if (membershipCreateStatus != MembershipCreateStatus.Success)
            {
                switch (membershipCreateStatus)
                {
                    case MembershipCreateStatus.DuplicateEmail:
                    case MembershipCreateStatus.DuplicateUserName:

                        this.ModelState.AddModelError("EmailValidation", "Email already registered");

                        break;
                }

                return this.CurrentUmbracoPage();
            }

            Partier partier = (Partier)membershipUser; // NOTE: considering moving the setters below into the Partier.cs

            partier.AssignRole(Partier.HostRoleAlias);

            partier.MarketingSource = registerHostForm.MarketingSource;

            //IMember member = this.Services.MemberService.GetByUsername(registerHostForm.EmailAddress);

            //// assign Party host role
            //this.Services.MemberService.AssignRole(member.Id, Partier.HostRoleAlias);           

            //// set the marketing source
            //member.Properties.Single(x => x.Alias == "marketingSource").Value = registerHostForm.MarketingSource;

            //// save member (so custom marketing source is set)
            //this.Services.MemberService.Save(member, true);

            // set memeber as online
            membersUmbracoMembershipProvider.GetUser(registerHostForm.EmailAddress, true);

            // log in
            FormsAuthentication.SetAuthCookie(registerHostForm.EmailAddress, true);

            return this.RedirectToUmbracoPage(this.CurrentPage.Children.Single(x => x.DocumentTypeAlias == RegisterHostPartyKit.Alias));
        }
    }
}
