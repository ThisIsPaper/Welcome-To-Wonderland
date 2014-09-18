
namespace Wonderland.Logic.Models.Forms
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class RegisterGuestBillingForm
    {        
        [Required]
        [DataType(DataType.Text)]
        public decimal Amount { get; set; }
    }
}
