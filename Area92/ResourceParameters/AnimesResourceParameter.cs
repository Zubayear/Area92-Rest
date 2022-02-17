using Newtonsoft.Json;

namespace Area92.ResourceParameters
{
    public class AnimesResourceParameter
    {
        const int maxSize = 100;
        private int size = 10;

        public int Page { get; set; } = 1;
        public int Size
        {
            get => size;
            set
            {
                if (value > 0)
                {
                    size = Math.Min(maxSize, value);
                }
            }
        }
        public int ReleaseYear { get; set; }
        public string SearchQuery { get; set; } = string.Empty;
        public string OrderBy { get; set; } = "IMDBRating";
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
