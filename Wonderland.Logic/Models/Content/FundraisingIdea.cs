
namespace Wonderland.Logic.Models.Content
{
    using Umbraco.Core.Models;
    using Umbraco.Web;
    using Wonderland.Logic.Models.Media;

    public class FundraisingIdea : SitePage
    {
        public new const string Alias = "FundraisingIdea";

        public FundraisingIdea(IPublishedContent content)
            : base(content)
        {
        }

        public string Heading
        {
            get
            {
                return this.GetPropertyValue<string>("heading");
            }
        }

        public Image Image
        {
            get
            {
                return new Image(this.GetPropertyValue<IPublishedContent>("image"));
            }
        }

        public string Copy
        {
            get
            {
                return this.GetPropertyValue<string>("copy");
            }
        }
    }
}
