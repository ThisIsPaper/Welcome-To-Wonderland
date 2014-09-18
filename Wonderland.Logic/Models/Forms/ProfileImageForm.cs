
namespace Wonderland.Logic.Models.Forms
{
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    public class ProfileImageForm
    {
        [Required]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase ProfileImage { get; set; }
    }
}
