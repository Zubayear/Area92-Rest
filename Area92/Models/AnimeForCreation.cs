using Newtonsoft.Json;

namespace Area92.Models
{
    // Request
    public class AnimeForCreation
    {
        public string Title { get; set; } = string.Empty;
        public int ReleaseYear { get; set; }
        public int EndYear { get; set; }
        public decimal IMDBRating { get; set; }
        public int NumberOfSeasons { get; set; }
        public string Language { get; set; } = string.Empty;
        public ICollection<string>? Genres { get; set; } = new List<string>();

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
