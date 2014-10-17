
namespace Wonderland.Logic.Models.Forms
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class ForgottenPasswordForm
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        [DisplayName("Email Address")]
        public string EmailAddress { get; set; }
    }
}
