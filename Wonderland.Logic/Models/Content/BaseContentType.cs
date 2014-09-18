
namespace Wonderland.Logic.Models.Content
{
    using Umbraco.Core.Models;
    using Umbraco.Core.Models.PublishedContent;
    using Umbraco.Web;
    using Umbraco.Web.Security;

    public abstract class BaseContentType : PublishedContentModel
    {
        private MembershipHelper membershipHelper = null;

        public BaseContentType(IPublishedContent content) 
            : base(content)
        {
        }

        /// <summary>
        /// to match the .Members property found in the SurfaceControllers and the Views
        /// </summary>
        protected MembershipHelper Members
        {
            get
            {
                if (this.membershipHelper == null)
                {
                    this.membershipHelper = new MembershipHelper(UmbracoContext.Current);
                }

                return this.membershipHelper;
            }
        }
    }
}
