
namespace Wonderland.Logic.Models.Forms
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class ProfileBillingAddressForm
    {
        [Required(ErrorMessage="Address Line 1 Required")]
        [MaxLength(100, ErrorMessage="Max Length of 100")]
        public string Address1 { get; set; }

        [MaxLength(100, ErrorMessage="Max Length of 100")]
        public string Address2 { get; set; }

        [Required(ErrorMessage="Town / City Required")]
        [MaxLength(40, ErrorMessage="Max Length of 40")]
        public string TownCity { get; set; }

        [Required(ErrorMessage="Postcode Required")]
        [MaxLength(10, ErrorMessage="Max Length of 10")]
        [DataType(DataType.PostalCode)]
        public string Postcode { get; set; }
    }
}