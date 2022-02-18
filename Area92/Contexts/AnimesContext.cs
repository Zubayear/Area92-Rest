using Area92.Entities;
using Microsoft.EntityFrameworkCore;

namespace Area92.Context
{
    public class AnimesContext : DbContext
    {
        public DbSet<Anime>? Animes { get; set; }

        public AnimesContext(DbContextOptions<AnimesContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var decimalProps = modelBuilder.Model
                .GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => (System.Nullable.GetUnderlyingType(p.ClrType) ?? p.ClrType) == typeof(decimal));

            foreach (var property in decimalProps)
            {
                property.SetPrecision(18);
                property.SetScale(2);
            }

            modelBuilder.Entity<Anime>()
                //.Property(e => e.Genres)
                //.HasConversion(
                //    v => string.Join(',', v),
                //    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries))
                .HasData(new Anime
                    {
                        Id = Guid.Parse("4d1be218-dc3d-46fe-b973-e7c6b5dbbd35"),
                        Title = "Demon Slayer: Kimetsu no Yaiba",
                        ReleaseYear = 2019,
                        IMDBRating = 8.7m,
                        IsEnded = false,
                        NumberOfSeasons = 2,
                        Language = "Japanese",
                        Genres = new List<string>
                        {
                            "Animation",
                            "Action",
                            "Adventure",
                            "Fantasy",
                            "Thriller"
                        }
                    },
                    new Anime
                    {
                        Id = Guid.Parse("d9a5b2f3-ac8f-4586-92b8-9e1cbc2f2d0b"),
                        Title = "Attack on Titan",
                        ReleaseYear = 2013,
                        IMDBRating = 9.0m,
                        IsEnded = false,
                        NumberOfSeasons = 4,
                        Language = "Japanese",
                        Genres = new List<string>
                        {
                            "Animation",
                            "Action",
                            "Adventure",
                            "Drama",
                            "Fantasy",
                            "Horror"
                        }
                    },
                    new Anime
                    {
                        Id = Guid.Parse("824b31c4-baae-4f82-b3c8-c4d4631e76ca"),
                        Title = "Hunter × Hunter",
                        ReleaseYear = 2011,
                        IMDBRating = 9.0m,
                        IsEnded = true,
                        NumberOfSeasons = 5,
                        Language = "Japanese",
                        Genres = new List<string>
                        {
                            "Animation",
                            "Action",
                            "Adventure",
                            "Comedy",
                            "Fantasy",
                        }
                    },
                    new Anime
                    {
                        Id = Guid.Parse("4287be31-a979-4601-9c28-960851bd564b"),
                        Title = "Jujutsu Kaisen",
                        ReleaseYear = 2020,
                        IMDBRating = 8.7m,
                        IsEnded = false,
                        NumberOfSeasons = 1,
                        Language = "Japanese",
                        Genres = new List<string>
                        {
                            "Animation",
                            "Action",
                            "Adventure",
                            "Fantasy",
                            "Thriller"
                        }
                    },
                    new Anime
                    {
                        Id = Guid.Parse("e53409c7-9825-4aaf-a05c-51690d386073"),
                        Title = "My Dress-Up Darling",
                        ReleaseYear = 2022,
                        IMDBRating = 8.4m,
                        IsEnded = false,
                        NumberOfSeasons = 1,
                        Language = "Japanese",
                        Genres = new List<string>
                        {
                            "Animation",
                            "Comedy",
                            "Romance",
                        }
                    },
                    new Anime
                    {
                        Id = Guid.Parse("389798ea-d4a5-4cd1-bfef-c13096ff1e9c"),
                        Title = "One-Punch Man",
                        ReleaseYear = 2015,
                        IMDBRating = 8.8m,
                        IsEnded = false,
                        NumberOfSeasons = 2,
                        Language = "Japanese",
                        Genres = new List<string>
                        {
                            "Animation",
                            "Action",
                            "Comedy",
                            "Fantasy",
                            "Sci-Fi"
                        }
                    },
                    new Anime
                    {
                        Id = Guid.Parse("c61d8992-b52a-45e8-a131-d08547ffca06"),
                        Title = "The Case Study of Vanitas",
                        ReleaseYear = 2021,
                        IMDBRating = 7.7m,
                        IsEnded = false,
                        NumberOfSeasons = 2,
                        Language = "Japanese",
                        Genres = new List<string>
                        {
                            "Animation",
                            "Sci-Fi",
                            "Mystery",
                            "Fantasy",
                            "Horror"
                        }
                    },
                    new Anime
                    {
                        Id = Guid.Parse("7cbae6db-90f4-4646-ba55-a002dd2a2f56"),
                        Title = "Naruto",
                        ReleaseYear = 2002,
                        IMDBRating = 8.3m,
                        IsEnded = true,
                        NumberOfSeasons = 22,
                        Language = "Japanese",
                        Genres = new List<string>
                        {
                            "Animation",
                            "Action",
                            "Adventure",
                            "Fantasy",
                            "Comedy"
                        }
                    }
                );
        }
    }
}