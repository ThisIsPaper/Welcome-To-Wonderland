
namespace Wonderland.Logic.Events
{
    using System;
    using Umbraco.Core;
    using Umbraco.Web;
    using Umbraco.Web.Routing;
    using Wonderland.Logic.Models.Content;

    public class SetSagePayResultContentFinder : ApplicationEventHandler
    {
        protected override void ApplicationStarting(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            ContentFinderResolver.Current.InsertTypeBefore<ContentFinderByNiceUrl, SagePayResultContentFinder>();
        }
    }

    public class SagePayResultContentFinder : IContentFinder
    {
        /// <summary>
        /// handles /Complete, /Cancelled and /Failed postfix url parts when applied to the RegisterGuest or Dontate pages
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
                    case "complete":
                    case "cancelled":

                        RegisterGuest registerGuest = (RegisterGuest)UmbracoContext.Current.ContentCache.GetSingleByXPath("//" + RegisterGuest.Alias);
                        Donate donate = (Donate)UmbracoContext.Current.ContentCache.GetSingleByXPath("//" + Donate.Alias);

                        if (urlParts[0] == registerGuest.Url.Trim('/'))
                        {
                            contentRequest.PublishedContent = registerGuest;
                            return true;

                        }

                        if (urlParts[0] == donate.Url.Trim('/'))
                        {
                            contentRequest.PublishedContent = donate;
                            return true;
                        }

                        break;
                }
            }

            return false;
        }
    }
}
