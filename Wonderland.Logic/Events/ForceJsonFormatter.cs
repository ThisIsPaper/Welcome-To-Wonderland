
namespace Wonderland.Logic.Events
{
    using System.Net.Http.Formatting;
    using System.Web.Http;
    using Umbraco.Core;

    /// <summary>
    /// ensures that json is returned for all api controller requests (allows for testing via browser gets)
    /// </summary>
    public class ForceJsonFormatter : ApplicationEventHandler
    {
        protected override void ApplicationStarting(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            GlobalConfiguration.Configuration.Formatters.Clear();
            GlobalConfiguration.Configuration.Formatters.Add(new JsonMediaTypeFormatter());         
        }
    }
}
