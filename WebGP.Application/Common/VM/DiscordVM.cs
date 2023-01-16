using System.Text.Json.Serialization;

namespace WebGP.Application.Common.VM;

public class DiscordVM
{
    [JsonPropertyName("discord_id")] public long DiscordID { get; set; }
    [JsonPropertyName("work")] public string Work { get; set; } = null!;
    [JsonPropertyName("role")] public string Role { get; set; } = null!;
    [JsonPropertyName("exp")] public int Exp { get; set; }
    [JsonPropertyName("phone")] public int? Phone { get; set; }
}