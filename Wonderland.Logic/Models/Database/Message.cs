
namespace Wonderland.Logic.Models.Database
{
    using System;
    using Umbraco.Core.Persistence;

    [TableName("wonderlandMessage")]
    [ExplicitColumns]
    public class Message
    {
        [Column("MemberId")]
        internal int MemberId { get; set; }

        [Column("Text")]
        internal string Text { get; set; }

        [Column("Image")]
        internal Guid Image { get; set; }

        [Column("Timestamp")]
        internal DateTime Timestamp { get; set; }
    }
}
