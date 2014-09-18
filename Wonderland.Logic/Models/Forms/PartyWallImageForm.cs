
namespace Wonderland.Logic.Models.Forms
{
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    public class PartyWallImageForm
    {
        [Required]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase PartyWallImage { get; set; }
    }
}
