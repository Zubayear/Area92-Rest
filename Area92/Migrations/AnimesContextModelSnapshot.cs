﻿// <auto-generated />
using System;
using Area92.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Area92.Migrations
{
    [DbContext(typeof(AnimesContext))]
    partial class AnimesContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Area92.Entities.Anime", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("GenresString")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("IMDBRating")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("IsEnded")
                        .HasColumnType("bit");

                    b.Property<string>("Language")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfSeasons")
                        .HasColumnType("int");

                    b.Property<int>("ReleaseYear")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Animes");

                    b.HasData(
                        new
                        {
                            Id = new Guid("4d1be218-dc3d-46fe-b973-e7c6b5dbbd35"),
                            GenresString = "Animation,Action,Adventure,Fantasy,Thriller",
                            IMDBRating = 8.7m,
                            IsEnded = false,
                            Language = "Japanese",
                            NumberOfSeasons = 2,
                            ReleaseYear = 2019,
                            Title = "Demon Slayer: Kimetsu no Yaiba"
                        },
                        new
                        {
                            Id = new Guid("d9a5b2f3-ac8f-4586-92b8-9e1cbc2f2d0b"),
                            GenresString = "Animation,Action,Adventure,Drama,Fantasy,Horror",
                            IMDBRating = 9.0m,
                            IsEnded = false,
                            Language = "Japanese",
                            NumberOfSeasons = 4,
                            ReleaseYear = 2013,
                            Title = "Attack on Titan"
                        },
                        new
                        {
                            Id = new Guid("824b31c4-baae-4f82-b3c8-c4d4631e76ca"),
                            GenresString = "Animation,Action,Adventure,Comedy,Fantasy",
                            IMDBRating = 9.0m,
                            IsEnded = true,
                            Language = "Japanese",
                            NumberOfSeasons = 5,
                            ReleaseYear = 2011,
                            Title = "Hunter × Hunter"
                        },
                        new
                        {
                            Id = new Guid("4287be31-a979-4601-9c28-960851bd564b"),
                            GenresString = "Animation,Action,Adventure,Fantasy,Thriller",
                            IMDBRating = 8.7m,
                            IsEnded = false,
                            Language = "Japanese",
                            NumberOfSeasons = 1,
                            ReleaseYear = 2020,
                            Title = "Jujutsu Kaisen"
                        },
                        new
                        {
                            Id = new Guid("e53409c7-9825-4aaf-a05c-51690d386073"),
                            GenresString = "Animation,Comedy,Romance",
                            IMDBRating = 8.4m,
                            IsEnded = false,
                            Language = "Japanese",
                            NumberOfSeasons = 1,
                            ReleaseYear = 2022,
                            Title = "My Dress-Up Darling"
                        },
                        new
                        {
                            Id = new Guid("389798ea-d4a5-4cd1-bfef-c13096ff1e9c"),
                            GenresString = "Animation,Action,Comedy,Fantasy,Sci-Fi",
                            IMDBRating = 8.8m,
                            IsEnded = false,
                            Language = "Japanese",
                            NumberOfSeasons = 2,
                            ReleaseYear = 2015,
                            Title = "One-Punch Man"
                        },
                        new
                        {
                            Id = new Guid("c61d8992-b52a-45e8-a131-d08547ffca06"),
                            GenresString = "Animation,Sci-Fi,Mystery,Fantasy,Horror",
                            IMDBRating = 7.7m,
                            IsEnded = false,
                            Language = "Japanese",
                            NumberOfSeasons = 2,
                            ReleaseYear = 2021,
                            Title = "The Case Study of Vanitas"
                        },
                        new
                        {
                            Id = new Guid("7cbae6db-90f4-4646-ba55-a002dd2a2f56"),
                            GenresString = "Animation,Action,Adventure,Fantasy,Comedy",
                            IMDBRating = 8.3m,
                            IsEnded = true,
                            Language = "Japanese",
                            NumberOfSeasons = 22,
                            ReleaseYear = 2002,
                            Title = "Naruto"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
