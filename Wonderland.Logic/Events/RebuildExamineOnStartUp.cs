namespace Wonderland.Logic.Events
{
		using Examine;
		using Umbraco.Core;
		using System.Linq;

    public class RebuildExamineOnStartup : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
						ExamineManager.Instance.IndexProviderCollection.ToList().ForEach(index => index.RebuildIndex());
        }

    }
}
