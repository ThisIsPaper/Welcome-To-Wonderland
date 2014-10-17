
namespace Wonderland.Logic.Models.Forms
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class LoginForm
    {
        [Required(ErrorMessage="Email Required")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        [DisplayName("Email Address")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage="Password Required")]
        [DataType(DataType.Password)]
        [MinLength(4, ErrorMessage="Min Length of 4")]
        public string Password { get; set; }
    }
}
