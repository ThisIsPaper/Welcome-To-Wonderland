
namespace Wonderland.Logic.Events
{
    using System;
    using System.Linq;
    using System.Web;
    using Umbraco.Core;
    using Umbraco.Core.Models;
    using Umbraco.Web.Routing;
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
                string partyUrlIdentifier = urlParts[1].ToLower();

                IMember partyHost = ApplicationContext
                                        .Current
                                        .Services
                                        .MemberService
                                        .GetMembersByMemberType(PartyHost.Alias)
                                        .SingleOrDefault(x => x.GetValue<string>(PartyHost.PartyUrlIdentifierAlias).ToLower() == partyUrlIdentifier);

                if (partyHost != null)
                {
                    // add to httpContext for consumption in the hijack controller
                    HttpContext.Current.Items["partyHostId"] = partyHost.Id;

                    // set to use the renderModel page
                    contentRequest.PublishedContent = contentRequest.RoutingContext.UmbracoContext.ContentCache.GetSingleByXPath("//" + Party.Alias);

                    // indicate suitable content has been found
                    return true;
                }
            }

            return false;
        }
    }
}
