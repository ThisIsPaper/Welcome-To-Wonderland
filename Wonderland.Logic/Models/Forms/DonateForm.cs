
namespace Wonderland.Logic.Models.Forms
{
    using System.ComponentModel.DataAnnotations;

    public class DonateForm
    {
        [Required]
        [DataType(DataType.Text)]
        [Range(1, 100000)]
        public decimal Amount { get; set; }

        //[Display("Allow Gift Aid")]
        public bool AllowGiftAid { get; set; }

        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(20)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(100)]
        public string Address1 { get; set; }

        [MaxLength(100)]
        public string Address2 { get; set; }

        [Required]
        [MaxLength(40)]
        public string TownCity { get; set; }

        //public string Country { get; set; }

        [Required]
        [MaxLength(10)]
        public string PostCode { get; set; }
    }
}
