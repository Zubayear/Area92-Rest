using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Area92.Entities
{
    [Table(name: "Animes")]
    public class Anime
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int ReleaseYear { get; set; }
        public decimal IMDBRating { get; set; }
        public bool IsEnded { get; set; }
        public int NumberOfSeasons { get; set; }
        public string Language { get; set; } = string.Empty;
        [NotMapped]
        public List<string> Genres { get; set; }
        public string GenresString
        {
            get { return String.Join(',', Genres); }
            set { Genres = value.Split(',').ToList(); }
        }
    }
}
