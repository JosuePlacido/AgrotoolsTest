using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Agrotools.Models
{
    public class User : IdentityUser
    {
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
