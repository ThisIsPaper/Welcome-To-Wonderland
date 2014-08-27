
namespace Wonderland.Logic.Models.Entities
{
    using Archetype.Models;

    public class QuestionAndAnswer
    {
        internal QuestionAndAnswer(ArchetypeFieldsetModel archetypeFieldsetModel)
        {
            this.Question = archetypeFieldsetModel.GetValue<string>("question");
            this.Answer = archetypeFieldsetModel.GetValue<string>("answer");
        }

        public string Question { get; internal set; }

        public string Answer { get; internal set; }
    }
}
