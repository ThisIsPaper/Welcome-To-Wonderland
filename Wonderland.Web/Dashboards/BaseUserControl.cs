

namespace Wonderland.Web.Dashboards
{
    using System.Web.UI;
    using Umbraco.Core;
    using Umbraco.Web;
    using Umbraco.Web.Security;

    public abstract class BaseUserControl : UserControl
    {
        private MembershipHelper members = null;

        protected DatabaseContext DatabaseContext
        {
            get
            {
                return ApplicationContext.Current.DatabaseContext;
            }
        }

        protected MembershipHelper Members
        {
            get
            {
                if (this.members == null)
                {
                    this.members = new MembershipHelper(UmbracoContext.Current);
                }

                return members;
            }
        }

    }
}