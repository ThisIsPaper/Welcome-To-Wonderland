
namespace Wonderland.Logic.Controllers.Api
{
    using Newtonsoft.Json.Linq;
    using System;
    using System.Web.Http;
    using Umbraco.Web.WebApi;
    using Wonderland.Logic.Models.Members;

    [MemberAuthorize(AllowType=PartyHost.Alias)]
    public class PartyApiController : UmbracoApiController
    {
        // ~/Umbraco/Api/PartyApi/UpdatePartyCopy?partyGuid=****
        [HttpPost]
        public bool UpdatePartyCopy([FromUri] Guid partyGuid, [FromBody] JObject data)
        {
            var partyCopy = data.GetValue("partyCopy");

            return false;
        }
    }
}
