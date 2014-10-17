
namespace Wonderland.Logic.Models.Forms
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class PartyCopyForm
    {
        [Required(ErrorMessage="Party Description Required")]
        [DisplayName("Party Description")]
        [MaxLength(600, ErrorMessage="Max Length of 600")]
        [DataType(DataType.MultilineText)]
        public string Copy { get; set; }
    }
}
