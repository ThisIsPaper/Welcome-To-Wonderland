
namespace Wonderland.Logic.Controllers.Api
{
    using System.Web.Http;
    using Umbraco.Web.WebApi;

    public class PostcodeAnywhereApiController : UmbracoApiController
    {
        [HttpGet]
        public object FindByPostcode([FromUri] string postcode)
        {
            return null;
        }
    }
}
