
namespace Wonderland.Logic.Models.Content
{
    using Umbraco.Core.Models;
    using Umbraco.Web;
    using Wonderland.Logic.Models.Media;

    public class FundraisingIdea : SitePage
    {
        // Document Type
        public new const string Alias = "FundraisingIdea";

        // Properties
        public const string HeadingAlias = "heading";
        public const string ImageAlias = "image";
        public const string CopyAlias = "copy";

        public FundraisingIdea(IPublishedContent content)
            : base(content)
        {
        }

        public string Heading
        {
            get
            {
                return this.GetPropertyValue<string>(FundraisingIdea.HeadingAlias);
            }
        }

        public Image Image
        {
            get
            {
                return new Image(this.GetPropertyValue<IPublishedContent>(FundraisingIdea.ImageAlias));
            }
        }

        public string Copy
        {
            get
            {
                return this.GetPropertyValue<string>(FundraisingIdea.CopyAlias);
            }
        }
    }
}
