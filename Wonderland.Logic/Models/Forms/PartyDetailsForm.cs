
namespace Wonderland.Logic.Models.Forms
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class PartyDetailsForm
    {
        [Required]
        [DisplayName("Your Party Name")]
        [DataType(DataType.Text)]
        public string PartyHeading { get; set; }

        [Required]
        [DisplayName("Party Date")]
        [DataType(DataType.Date)]
        public string PartyDate { get; set; }

        [Required]
        [DisplayName("Party Time")]
        [DataType(DataType.Time)]
        public string PartyTime { get; set; }

        [Required]
        [DisplayName("Address Line 1")]
        [DataType(DataType.Text)]
        public string Address1 { get; set; }

        [Required]
        [DisplayName("Address Line 2")]
        [DataType(DataType.Text)]
        public string Address2 { get; set; }

        [Required]
        [DisplayName("Town / City")]
        [DataType(DataType.Text)]
        public string TownCity { get; set; }

        [Required]
        [DisplayName("My Postcode Is")]
        [DataType(DataType.PostalCode)]
        public string Postcode { get; set; }
    }
}
