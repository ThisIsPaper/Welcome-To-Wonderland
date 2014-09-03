
namespace Wonderland.Logic.Events
{
    using Umbraco.Core;
    using Umbraco.Core.Models.PublishedContent;
    using Umbraco.Web.Routing;
    using Wonderland.Logic.Models.Content;

    public class SetPageNotFoundHandler : ApplicationEventHandler
    {
        protected override void ApplicationStarting(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            ContentLastChanceFinderResolver.Current.SetFinder(new PageNotFoundHandler());
        }
    }

    public class PageNotFoundHandler : IContentFinder
    {
        public bool TryFindContent(PublishedContentRequest contentRequest)
        {
            if (contentRequest.Is404)
            {
                // get the "Page Not Found" page
                contentRequest.PublishedContent = contentRequest.RoutingContext.UmbracoContext.ContentCache.GetSingleByXPath("//" + PageNotFound.Alias);

                // don't need to set this
                // contentRequest.SetResponseStatus(404);

                return true;
            }

            return false;
        }
    }
}
