using System.Text.Json.Serialization;

namespace WebGP.Application.Common.VM;

public class DiscordVm
{
    [JsonPropertyName("discord_id")] public long DiscordId { get; set; }
    [JsonPropertyName("uuid")] public string? Uuid { get; set; } = null!;
    [JsonPropertyName("static_id")] public uint? StaticId { get; set; }
    [JsonPropertyName("first_name")] public string? FirstName { get; set; } = null!;
    [JsonPropertyName("last_name")] public string? LastName { get; set; } = null!;
    [JsonPropertyName("work")] public string Work { get; set; } = null!;
    [JsonPropertyName("role")] public string Role { get; set; } = null!;
    [JsonPropertyName("level")] public int Level { get; set; }
    [JsonPropertyName("phone")] public int? Phone { get; set; }
}