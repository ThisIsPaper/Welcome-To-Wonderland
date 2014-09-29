
namespace Wonderland.Logic.Controllers.Render
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using umbraco;
    using Wonderland.Logic.Extensions;
    using Wonderland.Logic.Models.Content;
    using Wonderland.Logic.Models.Members;
    using Wonderland.Logic.Interfaces;
    using System.Web.Security;
    using Umbraco.Web.Models;

    public class ResetPasswordController : BaseRenderMvcController
    {
        public override ActionResult Index(RenderModel renderModel)
        {
            ResetPassword model = (ResetPassword)renderModel.Content;

            Guid forgottenPasswordGuid;
            if (Guid.TryParse(this.Request.QueryString["forgottenPasswordGuid"], out forgottenPasswordGuid))
            {                
                IPartier partier = this.Members.GetPartier(forgottenPasswordGuid);
                if (partier != null)
                {
                    // clear guid, so it can't be reused
                    partier.ForgottenPasswordGuid = Guid.Empty;

                    // send cookie
                    FormsAuthentication.SetAuthCookie(partier.Email, true);
                        
                    // cause redirect, so that the login takes effect
                    return this.Redirect(this.Umbraco.TypedContentSingleAtXPath("//" + ResetPassword.Alias).Url);
                }
            }
            
            if (this.Members.IsLoggedInPartier())
            {
                return this.CurrentTemplate(model);
            }
            else
            {
                // fallback and redirect to home
                return this.Redirect(Home.GetCurrentHome(model).Url);
            }
        }
    }
}
