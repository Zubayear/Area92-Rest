using Newtonsoft.Json;

namespace Area92.Models
{
    // Response
    public class Anime
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int ReleaseYear { get; set; }
        public decimal IMDBRating { get; set; }
        public bool IsEnded { get; set; }
        public int NumberOfSeasons { get; set; }
        public string Language { get; set; } = string.Empty;
        public ICollection<string>? Genres{ get; set; } = new List<string>();

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}