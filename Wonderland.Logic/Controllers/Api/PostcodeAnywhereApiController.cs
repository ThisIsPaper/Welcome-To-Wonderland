
namespace Wonderland.Logic.Controllers.Api
{
    using System.Web.Configuration;
    using System.Web.Http;
    using Umbraco.Web.WebApi;

    // http://wonderland.local/umbraco/Api/PostcodeAnywhereApi/FindByPostcode?postcode=EC1V1LJ
    public class PostcodeAnywhereApiController : UmbracoApiController
    {
        [HttpGet]
        public PostcodeAnywhere.Results[] FindByPostcode([FromUri] string postcode)
        {
            PostcodeAnywhere.WebService webService = new PostcodeAnywhere.WebService();

            return webService.FindByPostcode(WebConfigurationManager.AppSettings["PostcodeAnywhere:ServiceKey"], postcode, null);
        }
    }
}
