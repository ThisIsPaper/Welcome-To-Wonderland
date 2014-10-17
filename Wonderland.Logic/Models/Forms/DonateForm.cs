
namespace Wonderland.Logic.Models.Forms
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class DonateForm
    {
        [Required(ErrorMessage = "Amount Required")]
        [DataType(DataType.Text)]
        [Range(1, 10000, ErrorMessage = "Min £1, Max £10,000")]
        public decimal Amount { get; set; }

        //[Display("Allow Gift Aid")]
        public bool AllowGiftAid { get; set; }

        [Required(ErrorMessage = "First Name Required")]
        [MaxLength(20, ErrorMessage = "Max Length of 20")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name Required")]
        [MaxLength(20, ErrorMessage = "Max Length of 20")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Address Line 1 Required")]
        [MaxLength(100, ErrorMessage = "Max Length of 100")]
        public string Address1 { get; set; }

        [MaxLength(100, ErrorMessage = "Max Length of 100")]
        public string Address2 { get; set; }

        [Required(ErrorMessage = "Town / City Required")]
        [MaxLength(40, ErrorMessage = "Max Length of 40")]
        public string TownCity { get; set; }

        [Required(ErrorMessage = "Postcode Required")]
        [MaxLength(10, ErrorMessage = "Max Length of 10")]
        [DataType(DataType.PostalCode)]
        public string Postcode { get; set; }

        [Required]
        [HiddenInput(DisplayValue = false)]
        public Guid PartyGuid { get; set; }
    }
}
