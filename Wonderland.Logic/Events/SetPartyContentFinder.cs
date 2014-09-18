
namespace Wonderland.Logic.Events
{
    using System;
    using System.Web;
    using Umbraco.Core;
    using Umbraco.Web;
    using Umbraco.Web.Routing;
    using Umbraco.Web.Security;
    using Wonderland.Logic.Extensions;
    using Wonderland.Logic.Models.Content;
    using Wonderland.Logic.Models.Members;

    public class SetPartyContentFinder : ApplicationEventHandler
    {
        protected override void ApplicationStarting(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            ContentFinderResolver.Current.InsertTypeBefore<ContentFinderByNiceUrl, PartyContentFinder>();
        }
    }

    public class PartyContentFinder : IContentFinder
    {
        /// <summary>
        /// handles custom renderModel urls - if custom url is valid then the renderModel page is used together with a http context value indicating the renderModel host
        /// </summary>
        /// <param name="contentRequest"></param>
        /// <returns></returns>
        public bool TryFindContent(PublishedContentRequest contentRequest)
        {
            // chop up the url
            string[] urlParts = contentRequest.Uri.GetAbsolutePathDecoded().Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            // if we're looking for the renderModel page with an id
            if (urlParts.Length == 2 && urlParts[0].ToLower() == "party")
            {
                // attempt to get party host from the url identifier
                PartyHost partyHost = new MembershipHelper(UmbracoContext.Current).GetPartyHost(urlParts[1]);

                if (partyHost != null)
                {
                    // add to httpContext for consumption in the hijack controller
                    HttpContext.Current.Items["partyHostId"] = partyHost.Id;

                    // set to use the party page
                    contentRequest.PublishedContent = contentRequest.RoutingContext.UmbracoContext.ContentCache.GetSingleByXPath("//" + Party.Alias);

                    // indicate suitable content has been found
                    return true;
                }
            }

            return false;
        }
    }
}
