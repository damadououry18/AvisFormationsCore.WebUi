using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class MonDbContext :DbContext
    {
        public MonDbContext(DbContextOptions<MonDbContext> options) : base(options)
        {

        }
        public DbSet<Formation> Formations { get; set; }
        public DbSet<Avis> Avis { get; set; }
        public DbSet<ContactMessage>  Messages{ get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Formation>()
                .Property(f => f.Description)
                .HasMaxLength(500);

            modelBuilder.Entity<Formation>().HasData(
                new
                {
                    Id = 1,
                    Nom = "Créer un site web avec ASP.Net Core",
                    NomSeo = "asp-net-core",
                    Description = "Grace à cette formation vous saurez créer votre site web en peu de temps"
                },
                new
                {
                    Id = 2,
                    Nom = "Créer un site web avec PHP",
                    NomSeo = "php",
                    Description = "Grace à cette formation vous saurez créer votre site web en peu de temps"
                },
                new
                {
                    Id = 3,
                    Nom = "Devenez un pro du jardinage",
                    NomSeo = "apro-jardinage",
                    Description = "Apprenez à faire pousser des tomates, navets, courgettes et autre fruits et légumes savoureux toute l'année"
                },
                new
                {
                    Id = 4,
                    Nom = "Photo Pro",
                    NomSeo = "photo-pro",
                    Description = "un pro de la photo! vous saurez meme faire des selfies!"
                }
                );
        }
    }
}
