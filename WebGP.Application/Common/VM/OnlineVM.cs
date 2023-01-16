using System.Text.Json.Serialization;

namespace WebGP.Application.Common.VM;

public class OnlineVM
{
    [JsonPropertyName("timed_id")] public int TimedID { get; set; }
    [JsonPropertyName("uuid")] public string UUID { get; set; } = null!;
    [JsonPropertyName("first_name")] public string FirstName { get; set; } = null!;
    [JsonPropertyName("last_name")] public string LastName { get; set; } = null!;
    [JsonPropertyName("discord_id")] public long? DiscordID { get; set; }
    [JsonPropertyName("role")] public string Role { get; set; } = null!;
    [JsonPropertyName("work")] public string Work { get; set; } = null!;
    [JsonPropertyName("level")] public int Level { get; set; }
    [JsonPropertyName("skin_url")] public string SkinURL { get; set; } = null!;
}