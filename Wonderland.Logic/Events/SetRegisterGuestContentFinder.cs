
namespace Wonderland.Logic.Events
{
    using System;
    using Umbraco.Core;
    using Umbraco.Web;
    using Umbraco.Web.Routing;
    using Wonderland.Logic.Models.Content;

    public class SetRegisterGuestContentFinder : ApplicationEventHandler
    {
        protected override void ApplicationStarting(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            ContentFinderResolver.Current.InsertTypeBefore<ContentFinderByNiceUrl, RegisterGuestContentFinder>();
        }
    }

    public class RegisterGuestContentFinder : IContentFinder
    {
        /// <summary>
        /// handles /billing/ postfix url part when applied to the RegisterGuest page
        /// this is used so that distinct urls can be detected by the analytics
        /// </summary>
        /// <param name="contentRequest"></param>
        /// <returns></returns>
        public bool TryFindContent(PublishedContentRequest contentRequest)
        {
            // chop up the url
            string[] urlParts = contentRequest.Uri.GetAbsolutePathDecoded().Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            // we know hierarchy of register guest and donate pages, so expect only 2 url parts
            if (urlParts.Length == 2)
            {
                // only try processing if it ends with known string
                switch (urlParts[1])
                {
                    case "billing":

                        RegisterGuest registerGuest = (RegisterGuest)UmbracoContext.Current.ContentCache.GetSingleByXPath("//" + RegisterGuest.Alias);

                        if (urlParts[0] == registerGuest.Url.Trim('/'))
                        {
                            contentRequest.PublishedContent = registerGuest;
                            return true;
                        }

                        break;
                }
            }

            return false;
        }
    }
}
