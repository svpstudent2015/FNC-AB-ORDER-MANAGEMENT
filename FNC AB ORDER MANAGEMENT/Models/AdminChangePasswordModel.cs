using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FNC_AB_ORDER_MANAGEMENT.Models
{
    public class AdminChangePasswordModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [DisplayName("Nytt lösenord")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("Bekräfta nytt lösenord")]
        [Compare("NewPassword", ErrorMessage = "Det nya lösenordet och bekräftelse lösenordet matchar inte.")]
        public string ConfirmPassword { get; set; }

        public string Id { get; set; }
    }
}