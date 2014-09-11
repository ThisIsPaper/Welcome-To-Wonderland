
namespace Wonderland.Logic.Controllers.Api
{
    using Newtonsoft.Json.Linq;
    using System;
    using System.Web.Http;
    using Umbraco.Web.WebApi;
    using Wonderland.Logic.Extensions;
    using Wonderland.Logic.Models.Members;
    using Wonderland.Logic.Models.Api;

    [MemberAuthorize(AllowType=PartyHost.Alias)]
    public class PartyApiController : UmbracoApiController
    {
        // ~/Umbraco/Api/PartyApi/UpdatePartyCopy?partyGuid=****
        [HttpPost]
        public ApiResponse UpdatePartyCopy([FromUri] Guid partyGuid, [FromBody] JObject data)
        {
            ApiResponse apiResponse = new ApiResponse();

            PartyHost partyHost = (PartyHost)this.Members.GetCurrentMember();

            if (partyGuid == partyHost.PartyGuid)
            {
                partyHost.PartyCopy = data.GetValue("partyCopy").ToString();
                apiResponse.Success = true;
            }

            return apiResponse;
        }
    }
}
