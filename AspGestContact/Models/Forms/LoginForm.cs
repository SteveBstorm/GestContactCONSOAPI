using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspGestContact.Models.Forms
{
    public class LoginForm
    {
        [DisplayName("Email : ")]
        [Required(ErrorMessage = "Le champs 'Email' est requis")]
        [EmailAddress]
        public string Email { get; set; }
        [DisplayName("Mot de passe : ")]
        [Required(ErrorMessage = "Le champs 'Password' est requis")]
        [StringLength(20, MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Passwd { get; set; }
    }
}
