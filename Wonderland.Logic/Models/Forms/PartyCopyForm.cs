
namespace Wonderland.Logic.Models.Forms
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class PartyCopyForm
    {
        [Required]
        [DisplayName("Party Description")]
        [MaxLength(600)]
        [DataType(DataType.MultilineText)]
        public string Copy { get; set; }
    }
}
