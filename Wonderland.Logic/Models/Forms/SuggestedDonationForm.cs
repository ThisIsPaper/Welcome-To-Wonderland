
namespace Wonderland.Logic.Models.Forms
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class SuggestedDonationForm
    {
        [Required]
        [HiddenInput(DisplayValue = false)]
        public decimal SuggestedDonation { get; set; }
    }
}
