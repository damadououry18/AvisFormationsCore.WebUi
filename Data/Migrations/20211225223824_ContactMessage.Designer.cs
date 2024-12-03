﻿// <auto-generated />
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Data.Migrations
{
    [DbContext(typeof(MonDbContext))]
    [Migration("20211225223824_ContactMessage")]
    partial class ContactMessage
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Data.Avis", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Commentaire")
                        .HasColumnType("nvarchar(2000)")
                        .HasMaxLength(2000);

                    b.Property<int>("FormationId")
                        .HasColumnType("int");

                    b.Property<string>("NomUtilisateur")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Note")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("FormationId");

                    b.ToTable("Avis");
                });

            modelBuilder.Entity("Data.ContactMessage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nom")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("Data.Formation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomSeo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Formations");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Grace à cette formation vous saurez créer votre site web en peu de temps",
                            Nom = "Créer un site web avec ASP.Net Core",
                            NomSeo = "asp-net-core"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Grace à cette formation vous saurez créer votre site web en peu de temps",
                            Nom = "Créer un site web avec PHP",
                            NomSeo = "php"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Apprenez à faire pousser des tomates, navets, courgettes et autre fruits et légumes savoureux toute l'année",
                            Nom = "Devenez un pro du jardinage",
                            NomSeo = "apro-jardinage"
                        },
                        new
                        {
                            Id = 4,
                            Description = "un pro de la photo! vous saurez meme faire des selfies!",
                            Nom = "Photo Pro",
                            NomSeo = "photo-pro"
                        });
                });

            modelBuilder.Entity("Data.Avis", b =>
                {
                    b.HasOne("Data.Formation", "Formation")
                        .WithMany("Avis")
                        .HasForeignKey("FormationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
