using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Area92.Entities
{
    [Table(name: "Animes")]
    public class Anime
    {
        [Key] public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int ReleaseYear { get; set; }
        public decimal IMDBRating { get; set; }
        public bool IsEnded { get; set; }
        public int NumberOfSeasons { get; set; }
        public string Language { get; set; } = string.Empty;
        [NotMapped] public ICollection<string> Genres { get; set; }

        // Saving this in db
        public string GenresString
        {
            get => String.Join(',', Genres);
            set => Genres = value.Split(',').ToList();
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}