
namespace Wonderland.Logic.Controllers.Api
{
    using System.Web.Http;
    using Umbraco.Web.WebApi;
    using Wonderland.Logic.Models.Messages;

    public class FacebookApiController : UmbracoApiController
    {
        [HttpPost]
        public FormResponse RegisterGuest([FromBody] FacebookCredentials facebookCredentials)
        {
            FormResponse formResponse = new FormResponse();




            return formResponse;
        }
    }
}
