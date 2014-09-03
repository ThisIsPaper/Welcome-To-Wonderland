
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

    public class SetPartyHandler : ApplicationEventHandler
    {
        protected override void ApplicationStarting(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            ContentFinderResolver.Current.InsertTypeBefore<ContentFinderByNiceUrl, PartyHandler>();
        }
    }

    public class PartyHandler : IContentFinder
    {
        public bool TryFindContent(PublishedContentRequest contentRequest)
        {
            // chop up the url
            string[] urlParts = contentRequest.Uri.GetAbsolutePathDecoded().Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            // if we're looking for the party page with an id
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
