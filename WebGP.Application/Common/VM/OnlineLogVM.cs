using System.Text.Json.Serialization;

namespace WebGP.Application.Common.VM
{
    public class OnlineLogVM
    {
        [JsonPropertyName("day")] public string Day { get; set; } = null!;
        [JsonPropertyName("sec")] public int Seconds { get; set; }
    }
}
