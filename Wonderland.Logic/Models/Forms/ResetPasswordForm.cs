
namespace Wonderland.Logic.Models.Forms
{
    using System.ComponentModel.DataAnnotations;

    public class ResetPasswordForm
    {
        [Required(ErrorMessage = "New Password Required")]
        [DataType(DataType.Password)]
        [MinLength(4, ErrorMessage = "Max Length of 4")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password Required")]
        [DataType(DataType.Password)]
        [System.Web.Mvc.Compare("Password", ErrorMessage = "Passwords Not Matching")]
        public string ConfirmPassword { get; set; }
    }
}
