
namespace Wonderland.Logic.Models.Forms
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterHostPartyUrlForm
    {
        [Required]
        [Display(Name="Party Url")]
        [MaxLength(40)] // took a guess for this value
        public string PartyUrlIdentifier { get; set; }
    }
}
