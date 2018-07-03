using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppPortefolio.Models
{
    public class Utilizador
    {
        public Utilizador()
        {
            IsActive = true;
            DateCreated = DateTime.UtcNow;
        }

        [Key]
        public int ID { get; set; }
        public String Nome { get; set; }
        public String Username { get; set; }
        public String Email { get; set; }
        public String PasswordH { get; set; }

        public bool IsActive { get; set; }
        public DateTime DateCreated {get; set;}
        public DateTime DateDeleted { get; set; }
    }
}
