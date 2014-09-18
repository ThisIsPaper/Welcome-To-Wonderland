
namespace Wonderland.Logic.Models.Forms
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class FundraisingTargetForm
    {
        [Required]
        [HiddenInput(DisplayValue = false)]
        public decimal FundraisingTarget { get; set; }
    }
}
