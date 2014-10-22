
namespace Wonderland.Logic.Models.Database
{
    using System;
    using Umbraco.Core.Persistence;

    [TableName("wonderlandMessage")]
    [ExplicitColumns]
    [PrimaryKey("MessageId", autoIncrement=true)]
    public class MessageRow
    {
        public MessageRow()
        {
            this.Timestamp = DateTime.UtcNow;
        }

        [Column("MessageId")]
        public int MessageId { get; set; }

        [Column("MemberId")]
        public int MemberId { get; set; }

        [Column("Text")]
        public string Text { get; set; }

        [Column("Image")]
        public int? Image { get; set; }

        [Column("Timestamp")]
        public DateTime Timestamp { get; set; }

        public static explicit operator MessageRow(Donation_Message donation_Message)
        {
            return new MessageRow()
                        {
                            MessageId = donation_Message.Id,
                            MemberId = donation_Message.MemberId,
                            Text = donation_Message.Text,
                            Image = donation_Message.Image,
                            Timestamp = donation_Message.Timestamp
                        };
        }
    }
}
