
namespace Wonderland.Logic.Models.Forms
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using Wonderland.Logic.Models.Media;

    public class PartyImageForm
    {
        [ScaffoldColumn(false)]
        public IEnumerable<Image> DefaultImages { get; internal set; }

        [Required]
        [HiddenInput(DisplayValue = false)]
        public int PartyImage { get; set; }
    }
}
