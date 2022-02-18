using Newtonsoft.Json;

namespace Area92.ResourceParameters
{
    public class AnimesResourceParameter
    {
        private const int MaxSize = 100;
        private int _size = 10;

        public int Page { get; set; } = 1;

        public int Size
        {
            get => _size;
            set
            {
                if (value > 0)
                {
                    _size = Math.Min(MaxSize, value);
                }
            }
        }

        public int ReleaseYear { get; set; }
        public string SearchQuery { get; set; } = string.Empty;
        public string OrderBy { get; set; } = "IMDBRating";
        public string Fields { get; set; } = string.Empty;

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}