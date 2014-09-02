
namespace Wonderland.Logic.Models.Members
{
    using Umbraco.Core;
    using Umbraco.Core.Models;
    using Umbraco.Core.Models.PublishedContent;
    using Umbraco.Core.Services;

    public abstract class BaseMemberType : PublishedContentModel
    {
        private IMember member = null;

        public BaseMemberType(IPublishedContent content)
            : base(content)
        {
        }

        /// <summary>
        /// helper to get at the MemberService
        /// </summary>
        internal IMemberService MemberService
        {
            get
            {
                return ApplicationContext.Current.Services.MemberService;
            }
        }

        /// <summary>
        /// lazy load the member obj used to set properties to the db
        /// </summary>
        internal IMember Member
        {
            get
            {
                if (this.member == null)
                {
                    this.member = this.MemberService.GetById(this.Id);
                }

                return this.member;
            }
        }

        public string Username
        {
            get
            {
                return this.Member.Username;
            }
        }

        protected void SetPropertyValue(string propertyAlias, object propertyValue)
        {
            this.Member.SetValue(propertyAlias, propertyValue);
            this.MemberService.Save(this.member, false); // NOTE: false indicates that any Umbraco events watching wont get fired
        }
    }
}
