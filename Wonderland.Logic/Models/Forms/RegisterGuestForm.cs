
namespace Wonderland.Logic.Models.Forms
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class RegisterGuestForm
    {
        /// <summary>
        /// empty constructor for asp.net mvc
        /// </summary>
        public RegisterGuestForm()
        {
        }

        public RegisterGuestForm(Guid partyGuid)
        {
            this.PartyGuid = partyGuid;
        }

        [Required(ErrorMessage="Required Email")]
        [DataType(DataType.EmailAddress, ErrorMessage="Invalid Email")]
        //[Remote("ValidateIsEmailAvailable", "RegisterHostSurface", ErrorMessage="Email already registered")]
        [DisplayName("Email Address")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage="Required Password")]
        [DataType(DataType.Password)]
        [MinLength(4, ErrorMessage="Min Length of 4")]
        public string Password { get; set; }

        [ScaffoldColumn(false)]
        [DisplayName("How did you hear about us ?")]
        public string MarketingSource { get; set; }

        [Required]
        [HiddenInput(DisplayValue=false)]
        public Guid PartyGuid { get; set; }
    }
}
