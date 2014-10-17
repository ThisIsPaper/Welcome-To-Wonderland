
namespace Wonderland.Logic.Models.Forms
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class ProfileNamesForm
    {
        [Required(ErrorMessage="First Name Required")]
        [MaxLength(20, ErrorMessage="Max Length of 20")]
        public string FirstName { get; set; }

        [Required(ErrorMessage="Last Name Required")]
        [MaxLength(20, ErrorMessage="Max Length of 20")]
        public string LastName { get; set; }
    }
}
