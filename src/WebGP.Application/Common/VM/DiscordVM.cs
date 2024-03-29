﻿using System.Text.Json.Serialization;

namespace WebGP.Application.Common.VM;

public class DiscordVm
{
    [JsonPropertyName("discord_id")] public long DiscordId { get; set; }
    [JsonPropertyName("uuid")] public string? Uuid { get; set; }
    [JsonPropertyName("static_id")] public uint? StaticId { get; set; }
    [JsonPropertyName("first_name")] public string? FirstName { get; set; }
    [JsonPropertyName("last_name")] public string? LastName { get; set; }
    [JsonPropertyName("work")] public string? Work { get; set; }
    [JsonPropertyName("role")] public string? Role { get; set; }
    [JsonPropertyName("race")] public string? Race { get; set; }
    [JsonPropertyName("level")] public int Level { get; set; }
    [JsonPropertyName("phone")] public int? Phone { get; set; }
}