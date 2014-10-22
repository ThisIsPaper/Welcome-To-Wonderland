
namespace Wonderland.Logic.Models.Forms
{
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using System.Web.Mvc;

    public class PartyWallImageForm
    {
        [Required]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase PartyWallImage { get; set; }

        [HiddenInput(DisplayValue=false)]
        public int? LastPartyWallImageId { get; set; }
    }
}
