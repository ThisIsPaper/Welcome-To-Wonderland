
namespace Wonderland.Logic.Models.Forms
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using System.Web.Mvc;

    public class ProfileImageForm
    {
        [Required]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase ProfileImage { get; set; }
    }
}
