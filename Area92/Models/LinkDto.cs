using Newtonsoft.Json;

namespace Area92.Models;

public class LinkDto
{
    public LinkDto(string? href, string rel, string method)
    {
        Href = href ?? throw new ArgumentNullException(nameof(href));
        Rel = rel ?? throw new ArgumentNullException(nameof(rel));
        Method = method ?? throw new ArgumentNullException(nameof(method));
    }

    public string Href { get; set; }
    public string Rel { get; set; }
    public string Method { get; set; }
    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}