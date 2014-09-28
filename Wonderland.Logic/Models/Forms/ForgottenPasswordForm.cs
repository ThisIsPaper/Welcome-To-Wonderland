
namespace Wonderland.Logic.Models.Forms
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class ForgottenPasswordForm
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Email Address")]
        public string EmailAddress { get; set; }
    }
}
