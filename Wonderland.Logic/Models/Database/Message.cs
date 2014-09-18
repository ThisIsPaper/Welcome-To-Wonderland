
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
        internal string Image { get; set; }

        [Column("Timestamp")]
        internal DateTime Timestamp { get; set; }

        public static explicit operator Message(Donation_Message donation_Message)
        {
            return new Message()
                        {
                            MemberId = donation_Message.MemberId,
                            Text = donation_Message.Text,
                            Image = donation_Message.Image,
                            Timestamp = donation_Message.Timestamp
                        };
        }
    }
}
