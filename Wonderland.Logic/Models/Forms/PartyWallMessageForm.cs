
namespace Wonderland.Logic.Models.Forms
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class PartyWallMessageForm
    {
        [DataType(DataType.MultilineText)]
        [MaxLength(600, ErrorMessage="Max Length of 600")]
        public string Message { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string PartyWallImage { get; set; }
    }
}
