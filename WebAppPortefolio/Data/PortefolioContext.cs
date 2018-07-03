using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppPortefolio.Models;

namespace WebAppPortefolio.Data
{
    public class PortefolioContext : DbContext
    {
        public PortefolioContext(DbContextOptions<PortefolioContext> options)
            : base(options)
        {
        }

        public DbSet<Utilizador> Utilizadores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuiler)
        {
            modelBuiler.Entity<Utilizador>().ToTable("Utilizador");
        }
    }
}
