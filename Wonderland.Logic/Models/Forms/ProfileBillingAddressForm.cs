
namespace Wonderland.Logic.Models.Forms
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class ProfileBillingAddressForm
    {
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
        public string Postcode { get; set; }
    }
}



