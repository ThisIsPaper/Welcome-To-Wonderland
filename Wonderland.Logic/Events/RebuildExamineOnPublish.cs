namespace Wonderland.Logic.Events
{
    using Examine;
    using System.Linq;
    using Umbraco.Core;
    using Umbraco.Core.Events;
    using Umbraco.Core.Models;
    using Umbraco.Core.Publishing;
    using Umbraco.Core.Services;

    /// <summary>
    /// HACK: this is an attempt to ensure content changes via the CMS cause all servers to update their Lucene indexes
    /// </summary>
    public class RebuildExamineOnPublish : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            ContentService.Published += ContentService_Published;         
        }

        private void ContentService_Published(IPublishingStrategy sender, PublishEventArgs<IContent> e)
        {
            ExamineManager.Instance.IndexProviderCollection.ToList().ForEach(index => index.RebuildIndex());
        }
    }
}
