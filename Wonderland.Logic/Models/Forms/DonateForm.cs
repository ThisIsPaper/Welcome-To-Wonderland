
namespace Wonderland.Logic.Models.Forms
{
    using System.ComponentModel.DataAnnotations;

    public class DonateForm
    {
        [Required]
        [DataType(DataType.Text)]
        public decimal Amount { get; set; }

        //[Display("Allow Gift Aid")]
        public bool AllowGiftAid { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Address1 { get; set; }

        public string Address2 { get; set; }

        [Required]
        public string TownCity { get; set; }

        //public string Country { get; set; }

        [Required]
        public string PostCode { get; set; }
    }
}
