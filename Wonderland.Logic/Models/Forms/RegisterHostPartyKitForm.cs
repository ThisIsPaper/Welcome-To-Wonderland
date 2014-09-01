
namespace Wonderland.Logic.Models.Forms
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterHostPartyKitForm
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName{ get; set; }

        [Required]
        public string Address1 { get; set; }
        
        public string Address2 { get; set; }

        [Required]
        public string TownCity { get; set; }

        //public string Country { get; set; }

        [Required]
        public string PostCode { get; set; }
    }
}
