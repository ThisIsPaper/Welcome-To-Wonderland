namespace Wonderland.Logic.Events
{
    using Examine;
    using System.Linq;
    using Umbraco.Core;
    using Umbraco.Web.Cache;

    /// <summary>
    /// HACK: this is an attempt to ensure content changes via the CMS cause all servers to update their Lucene indexes
    /// </summary>
    public class RebuildExamineOnCacheUpdated : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            PageCacheRefresher.CacheUpdated += this.PageCacheRefresher_CacheUpdated;
        }

        private void PageCacheRefresher_CacheUpdated(PageCacheRefresher sender, Umbraco.Core.Cache.CacheRefresherEventArgs e)
        {
            ExamineManager.Instance.IndexProviderCollection.ToList().ForEach(index => index.RebuildIndex());
        }
    }
}
