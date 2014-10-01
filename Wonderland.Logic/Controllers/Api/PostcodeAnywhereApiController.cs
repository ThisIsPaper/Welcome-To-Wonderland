
namespace Wonderland.Logic.Controllers.Api
{
    using System.Web.Configuration;
    using System.Web.Http;
    using Umbraco.Web.WebApi;
    
    public class PostcodeAnywhereApiController : UmbracoApiController
    {
        private string ServiceKey
        {
            get
            {
                return WebConfigurationManager.AppSettings["PostcodeAnywhere:ServiceKey"];
            }
        }

        /// <summary>
        /// http://www.postcodeanywhere.co.uk/support/webservice/postcodeanywhere/interactive/findbypostcode/1/
        /// http://wonderland.local/umbraco/Api/PostcodeAnywhereApi/FindByPostcode?postcode=EC1V1LJ
        /// </summary>
        /// <param name="postcode"></param>
        /// <returns></returns>
        [HttpGet]
        public PostcodeAnywhere.FindByPostcode.Results[] FindByPostcode([FromUri] string postcode)
        {
            PostcodeAnywhere.FindByPostcode.WebService webService = new PostcodeAnywhere.FindByPostcode.WebService();

            return webService.FindByPostcode(this.ServiceKey, postcode, null);
        }

        /// <summary>
        /// http://www.postcodeanywhere.co.uk/Support/WebService/PostcodeAnywhere/Interactive/RetrieveById/1.3/
        /// http://wonderland.local/umbraco/Api/PostcodeAnywhereApi/RetrieveById?idField=8175153.00
        /// </summary>
        /// <param name="idField"></param>
        /// <returns></returns>
        [HttpGet]
        public PostcodeAnywhere.RetrieveById.Results[] RetrieveById([FromUri] string idField)
        {
            PostcodeAnywhere.RetrieveById.WebService webService = new PostcodeAnywhere.RetrieveById.WebService();

            return webService.RetrieveById(this.ServiceKey, idField, "English", null);
        }
    }
}
