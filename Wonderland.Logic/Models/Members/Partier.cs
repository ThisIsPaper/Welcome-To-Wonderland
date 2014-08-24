

namespace Wonderland.Logic.Models.Members
{
    using Umbraco.Core.Models;
    using Umbraco.Core.Models.PublishedContent;

    public class Partier : PublishedContentModel
    {
        public const string Alias = "Partier";

        public Partier(IPublishedContent content)
            : base(content)
        {
        }
    }
}
