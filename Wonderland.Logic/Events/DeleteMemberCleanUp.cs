
namespace Wonderland.Logic.Events
{
    using System.Linq;
    using System.Web;
    using Umbraco.Core;
    using Umbraco.Core.Events;
    using Umbraco.Core.Models;
    using Umbraco.Core.Publishing;
    using Umbraco.Core.Services;
    using Umbraco.Web;
    using Wonderland.Logic.Interfaces;
    using Wonderland.Logic.Models.Members;

    public class DeleteMemberCleanUp : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            MemberService.Deleting += MemberService_Deleting;            
        }

        private void MemberService_Deleting(IMemberService sender, DeleteEventArgs<IMember> e)
        {
            UmbracoHelper umbracoHelper = new UmbracoHelper(UmbracoContext.Current);

            foreach(IMember member in e.DeletedEntities)
            {
                switch (member.ContentTypeAlias)
                {
                    case PartyHost.Alias:
                    
                        PartyHost partyHost = (PartyHost)umbracoHelper.TypedMember(member.Id);

                        // remove party image media item
                        partyHost.PartyImage = null;

                        // delete all guests ?

                        break;

                    default:
                        
                        IPartier partier = (IPartier)umbracoHelper.TypedContent(member.Id);

                        // delete media item for profile Image
                        partier.ProfileImage = null;

                        // get all wonderlandMessage, remove or mark as deleted ? (media will be on s3, so delete from there)
                        // remove from wonderlandMemberParty

                        break;
                }
            }
        }
    }
}
