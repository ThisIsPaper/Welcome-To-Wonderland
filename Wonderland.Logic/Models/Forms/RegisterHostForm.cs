
namespace Wonderland.Logic.Models.Forms
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class RegisterHostForm
    {
        [Required(ErrorMessage="Required Email")]
        [DataType(DataType.EmailAddress, ErrorMessage="Invalid Email")]
        //[Remote("ValidateIsEmailAvailable", "RegisterHostSurface", ErrorMessage="Email already registered")]
        [DisplayName("Email Address")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage="Required Password")]
        [DataType(DataType.Password)]
        [MinLength(4, ErrorMessage="Min Length of 4")]
        public string Password { get; set; }

        [ScaffoldColumn(false)]
        [DisplayName("How did you hear about us ?")]
        public string MarketingSource { get; set; }
    }
}
