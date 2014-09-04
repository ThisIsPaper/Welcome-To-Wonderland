
namespace Wonderland.Logic.Events
{
    using Umbraco.Core;
    using Umbraco.Web.Routing;
    using Wonderland.Logic.Models.Content;

    public class SetPageNotFoundContentFinder : ApplicationEventHandler
    {
        protected override void ApplicationStarting(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            ContentLastChanceFinderResolver.Current.SetFinder(new PageNotFoundContentFinder());
        }
    }

    public class PageNotFoundContentFinder : IContentFinder
    {
        public bool TryFindContent(PublishedContentRequest contentRequest)
        {
            if (contentRequest.Is404)
            {
                // get the "Page Not Found" page
                contentRequest.PublishedContent = contentRequest.RoutingContext.UmbracoContext.ContentCache.GetSingleByXPath("//" + PageNotFound.Alias);

                return true;
            }

            return false;
        }
    }
}
