
namespace Wonderland.Logic.Models.Forms
{
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    public class CustomPartyImageForm
    {
        [Required]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase CustomPartyImage { get; set; }
    }
}
