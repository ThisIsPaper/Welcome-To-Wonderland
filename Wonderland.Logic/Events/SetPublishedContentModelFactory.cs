
namespace Wonderland.Logic.Events
{
    using System;
    using System.Collections.Generic;
    using Umbraco.Core;
    using Umbraco.Core.Models.PublishedContent;

    public class SetPublishedContentModelFactory : ApplicationEventHandler
    {
        protected override void ApplicationStarting(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            IEnumerable<Type> types = PluginManager.Current.ResolveTypes<PublishedContentModel>();
            PublishedContentModelFactory factory = new PublishedContentModelFactory(types);
            PublishedContentModelFactoryResolver.Current.SetFactory(factory);
        }
    }
}
