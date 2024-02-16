using ApiPraticien.Models;
using ApiPraticien.Models.Praticien;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace ApiPraticien.Data
{
    public class PraticienDbContext : DbContext
    {
        public PraticienDbContext(DbContextOptions<PraticienDbContext> options) : base(options)
        {
        }

        public DbSet<Praticien> Praticiens { get; set; }
       // public DbSet<Agenda> Agendas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuration de l'entité Praticien si nécessaire
        }
    }
}
