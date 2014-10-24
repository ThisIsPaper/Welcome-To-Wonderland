namespace Wonderland.Logic.Events
{
    using Examine;
    using System.Linq;
    using Umbraco.Core;

    /// <summary>
    /// 
    /// </summary>
    public class RebuildExamineOnStartup : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
			ExamineManager.Instance.IndexProviderCollection.ToList().ForEach(index => index.RebuildIndex());
        }
    }
}
