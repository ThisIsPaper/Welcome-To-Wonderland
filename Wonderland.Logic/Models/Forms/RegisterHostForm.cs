
namespace Wonderland.Logic.Models.Forms
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class RegisterHostForm
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        //[Remote("ValidateIsEmailAvailable", "RegisterHostSurface", ErrorMessage="Email already registered")]
        [DisplayName("Email Address")]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [ScaffoldColumn(false)]
        [DisplayName("How did you hear about us ?")]
        public string MarketingSource { get; set; }
    }
}
