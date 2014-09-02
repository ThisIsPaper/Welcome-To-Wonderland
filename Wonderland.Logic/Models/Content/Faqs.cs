
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
        // Document Type
        public new const string Alias = "FAQs";

        // Properties
        public const string PageHeadingAlias = "pageHeading";
        public const string QuestionsAndAnswersAlias = "questionsAndAnswers";
        public const string CopyAlias = "copy";

        public FAQs(IPublishedContent content)
            : base(content)
        {
        }

        public string PageHeading
        {
            get
            {
                return this.GetPropertyValue<string>(FAQs.PageHeadingAlias);
            }
        }

        public IEnumerable<QuestionAndAnswer> QuestionsAndAnswers
        {
            get
            {
                return this.GetPropertyValue<ArchetypeModel>(FAQs.QuestionsAndAnswersAlias).Select(x => new QuestionAndAnswer(x));
            }
        }

        public string Copy
        {
            get
            {
                return this.GetPropertyValue<string>(FAQs.CopyAlias);
            }
        }
    }
}
