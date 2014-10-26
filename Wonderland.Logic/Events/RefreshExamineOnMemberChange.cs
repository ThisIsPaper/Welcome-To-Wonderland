
namespace Wonderland.Logic.Events
{
    using AgeBase.ExtendedDistributedCalling.Services;
    using Umbraco.Core;
    using Umbraco.Core.Services;

    public class RefreshExamineOnMemberChange : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            MemberService.Created += delegate { ExtendedDistributedCallingService.Refresh(); };
            MemberService.Deleted += delegate { ExtendedDistributedCallingService.Refresh(); };
            MemberService.Saved += delegate { ExtendedDistributedCallingService.Refresh(); };
        }
    }
}
