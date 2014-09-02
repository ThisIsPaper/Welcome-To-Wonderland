
namespace Wonderland.Logic.Controllers.Render
{
    using System;
    using System.Web.Mvc;
    using Umbraco.Web.Mvc;
    using Wonderland.Logic.Enums;
    using Wonderland.Logic.Models.Content;
    using Wonderland.Logic.Models.Members;

    public class RegisterHostPartyUrlController : RenderMvcController
    {
        //[MemberAuthorize(AllowGroup = Partier.HostRoleAlias)]
        public ActionResult RegisterHostPartyUrl()
        {
            RegisterHostPartyUrl model = (RegisterHostPartyUrl)this.CurrentPage;

            // TODO: get party url identifier for the current host

            return this.CurrentTemplate(model);
        }
    }
}
