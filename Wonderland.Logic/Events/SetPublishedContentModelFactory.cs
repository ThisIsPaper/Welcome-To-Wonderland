
namespace Wonderland.Logic.Events
{
    using Umbraco.Core;
    using Umbraco.Core.Models.PublishedContent;

    public class SetPublishedContentModelFactory : ApplicationEventHandler
    {
        protected override void ApplicationStarting(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            var types = PluginManager.Current.ResolveTypes<PublishedContentModel>();
            var factory = new PublishedContentModelFactory(types);
            PublishedContentModelFactoryResolver.Current.SetFactory(factory);
        }
    }
}
