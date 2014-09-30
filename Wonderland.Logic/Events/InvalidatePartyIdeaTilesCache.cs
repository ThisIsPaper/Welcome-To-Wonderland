
namespace Wonderland.Logic.Events
{
    using System.Web;
    using Umbraco.Core;
    using Umbraco.Core.Events;
    using Umbraco.Core.Models;
    using Umbraco.Core.Services;

    public class InvalidatePartyIdeaTilesCache : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            ContentService.Published += this.ContentService_Published;
        }

        private void ContentService_Published(Umbraco.Core.Publishing.IPublishingStrategy sender, PublishEventArgs<IContent> e)
        {
            // TODO: only wipe if party ideas, or a descendant of that is being published

            HttpContext.Current.Cache.Remove("PartyIdeaTiles");
        }
    }
}
