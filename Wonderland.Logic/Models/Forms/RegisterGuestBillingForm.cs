
namespace Wonderland.Logic.Models.Forms
{

    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class RegisterGuestBillingForm
    {
        [Required]
        [DataType(DataType.Text)]
        [Range(0, 100000)] // TODO: skip sage pay and go to party page
        public decimal Amount { get; set; }

        [DataType(DataType.MultilineText)]
        [MaxLength(600)]
        public string Message { get; set; }

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
        public string Postcode { get; set; }

        [Required]
        [HiddenInput(DisplayValue = false)]
        public Guid PartyGuid { get; set; }

    }
}
