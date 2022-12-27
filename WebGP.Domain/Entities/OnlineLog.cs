using System.Text.Json.Serialization;

namespace WebGP.Domain.Entities
{
    public class OnlineLog
    {
        [JsonPropertyName("day")] public DateTime Day { get; set; }
        [JsonPropertyName("sec")] public int Seconds { get; set; }
    }
}
