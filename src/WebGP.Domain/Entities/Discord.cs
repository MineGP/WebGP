namespace WebGP.Domain.Entities;

public class Discord
{
    public long DiscordId { get; set; }

    public string Uuid { get; set; } = null!;

    public DateTime LastUpdate { get; set; }
}