
namespace Wonderland.Logic.Models.Forms
{
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    public class ProfileImageForm
    {
        [DataType(DataType.Upload)]
        public HttpPostedFileBase ProfileImage { get; set; }
    }
}
