using ApiRdv.Models;
using ApiRdv.Models.Rdvs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace ApiRdv.Data
{
    public class RdvDbContext : DbContext
    {
        public RdvDbContext(DbContextOptions<RdvDbContext> options) : base(options)
        {
        }
       
        public DbSet<Rdv> Rdvs { get; set; }
       
        public object Praticiens { get; internal set; }

        // Vous pouvez ajouter d'autres DbSets pour d'autres entités...

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            base.OnModelCreating(modelBuilder);
        }
    }
}
