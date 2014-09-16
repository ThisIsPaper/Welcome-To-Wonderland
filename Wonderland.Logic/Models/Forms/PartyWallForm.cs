
namespace Wonderland.Logic.Models.Forms
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using System.Web.Mvc;

    public class PartyWallForm
    {
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }

        [DataType(DataType.Upload)]
        public HttpPostedFile WallImage { get; set; }
    }
}
