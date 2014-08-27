
namespace Wonderland.Logic.Models.Content
{
    using Archetype.Models;
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Core.Models;
    using Umbraco.Web;
    using Wonderland.Logic.Models.Entities;

    public class FAQs : SitePage
    {
        public new const string Alias = "FAQs";

        public FAQs(IPublishedContent content)
            : base(content)
        {
        }

        public string PageHeading
        {
            get
            {
                return this.GetPropertyValue<string>("pageHeading");
            }
        }

        public IEnumerable<QuestionAndAnswer> QuestionsAndAnswers
        {
            get
            {
                return this.GetPropertyValue<ArchetypeModel>("questionsAndAnswers").Select(x => new QuestionAndAnswer(x));
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
