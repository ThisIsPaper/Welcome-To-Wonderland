
namespace Wonderland.Logic.Models.Forms
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class PartyDetailsForm
    {
        [Required]
        [DisplayName("Your Party Name")]
        public string PartyHeading { get; set; }

        [Required]
        [DisplayName("Party Date")]
        public string PartyDate { get; set; }

        [Required]
        [DisplayName("Party Time")]
        public string PartyTime { get; set; }

        [Required]
        [DisplayName("Address Line 1")]
        public string Address1 { get; set; }

        [Required]
        [DisplayName("Address Line 2")]
        public string Address2 { get; set; }

        [Required]
        [DisplayName("Town / City")]
        public string TownCity { get; set; }

        [Required]
        [DisplayName("My Postcode Is")]
        public string Postcode { get; set; }
    }
}
