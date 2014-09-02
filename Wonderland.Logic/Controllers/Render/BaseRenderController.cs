
namespace Wonderland.Logic.Controllers.Render
{
    using Umbraco.Web;
    using Umbraco.Web.Mvc;
    using Umbraco.Web.Security;

    public abstract class BaseRenderController : RenderMvcController
    {
        private MembershipHelper membershipHelper = null;

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
