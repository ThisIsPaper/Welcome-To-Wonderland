
namespace Wonderland.Logic.Models.Media
{
    using Umbraco.Core;
    using Umbraco.Core.Models;
    using Umbraco.Core.Services;
    using Umbraco.Web;
    using Umbraco.Web.Security;
    using Wonderland.Logic.Models;

    public abstract class BaseMediaType : NonSerializablePublishedContentModel
    {
        private UmbracoHelper umbracoHelper = null;
        private MembershipHelper membershipHelper = null;
        private IMedia media = null;

        protected UmbracoHelper Umbraco
        {
            get
            {
                if (this.umbracoHelper == null)
                {
                    this.umbracoHelper = new UmbracoHelper(UmbracoContext.Current);
                }

                return this.umbracoHelper;
            }
        }

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
                if (this.membershipHelper == null)
                {
                    this.membershipHelper = new MembershipHelper(UmbracoContext.Current);
                }

                return this.membershipHelper;
            }
        }

        internal IMemberService MemberService
        {
            get
            {
                return ApplicationContext.Current.Services.MemberService;
            }
        }

        protected IMediaService MediaService
        {
            get
            {
                return ApplicationContext.Current.Services.MediaService;
            }
        }

        internal IMedia Media
        {
            get
            {
                if (this.media == null)
                {
                    this.media = this.MediaService.GetById(this.Id);
                }

                return this.media;
            }
        }

        public BaseMediaType(IPublishedContent content)
            : base(content)
        {
        }

        protected void SetPropertyValue(string propertyAlias, object propertyValue)
        {
            this.Media.SetValue(propertyAlias, propertyValue);
            this.MediaService.Save(this.Media, 0, true);
        }
    }
}
