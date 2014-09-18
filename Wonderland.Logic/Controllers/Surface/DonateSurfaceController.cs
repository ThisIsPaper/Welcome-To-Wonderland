
namespace Wonderland.Logic.Controllers.Surface
{
    using System;
    using System.Web.Mvc;
    using Umbraco.Web.Mvc;
    using Wonderland.Logic.Models.Content;

    public class DonateSurfaceController : SurfaceController
    {
        /// <summary>
        /// Handles inbound links to the donate page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult NavigateToDonateUrl(Guid partyGuid)
        {
            return this.Redirect(this.Umbraco.TypedContentSingleAtXPath("//" + Donate.Alias).Url + "?partyGuid=" + partyGuid.ToString());
        }
    }
}
