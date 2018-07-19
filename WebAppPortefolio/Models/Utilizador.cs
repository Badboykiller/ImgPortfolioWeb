using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppPortefolio.Models
{
    public class Utilizador : IdentityUser
    {
        public Utilizador()
        {
            IsActive = true;
            DateCreated = DateTime.UtcNow;
        }

        public String Nome { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateCreated {get; set;}
        public DateTime DateDeleted { get; set; }
    }
}
