
namespace Wonderland.Logic.Models.Database
{
    using System;
    using Umbraco.Core.Persistence;

    [TableName("wonderlandWall")]
    [ExplicitColumns]
    internal class Wall
    {
        [Column("MemberId")]
        internal int MemberId { get; set; }

        [Column("Message")]
        internal string Message { get; set; }

        [Column("Image")]
        internal Guid Image { get; set; }

        [Column("Timestamp")]
        internal DateTime Timestamp { get; set; }
    }
}
