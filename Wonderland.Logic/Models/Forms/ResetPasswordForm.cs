
namespace Wonderland.Logic.Models.Forms
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class ResetPasswordForm
    {
        [Required]
        [DataType(DataType.Password)]
        [MinLength(4)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [System.Web.Mvc.Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
