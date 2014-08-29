
namespace Wonderland.Logic.Models.Forms
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class RegisterHostForm
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        public IEnumerable<string> MarketingSource { get; set; }
    }
}
