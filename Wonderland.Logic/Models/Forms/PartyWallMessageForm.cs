
namespace Wonderland.Logic.Models.Forms
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class PartyWallMessageForm
    {
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string PartyWallImage { get; set; }
    }
}
