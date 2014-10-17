
namespace Wonderland.Logic.Models.Forms
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class PartyDetailsForm
    {
        [Required(ErrorMessage="Party Name Required")]
        [DisplayName("Your Party Name")]
        [DataType(DataType.Text)]
        public string PartyHeading { get; set; }

        [Required(ErrorMessage="Party Date / Time Required")]
        [DisplayName("Party Date and Time")]
        [DataType(DataType.DateTime)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime PartyDateTime { get; set; }

        [Required(ErrorMessage="Address Line 1 Required")]
        [DisplayName("Address Line 1")]
        [DataType(DataType.Text)]
        public string Address1 { get; set; }

        [DisplayName("Address Line 2")]
        [DataType(DataType.Text)]
        public string Address2 { get; set; }

        [Required(ErrorMessage="Town / Citry Required")]
        [DisplayName("Town / City")]
        [DataType(DataType.Text)]
        public string TownCity { get; set; }

        [Required(ErrorMessage="Postcode Required")]
        [DisplayName("My Postcode Is")]
        [DataType(DataType.PostalCode)]
        public string Postcode { get; set; }
    }
}
