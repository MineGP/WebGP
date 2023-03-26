namespace WebGP.Domain.Entities;

public partial class Role
{
    public int Id { get; set; }

    public string Color { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int Permissions { get; set; }

    public long PermMenu { get; set; }

    public long PermMenuLocal { get; set; }

    public int IdGroup { get; set; }

    public sbyte Static { get; set; }

    public long? DiscordRole { get; set; }

    public string? HeadData { get; set; }

    public DateTime LastUpdate { get; set; }
}
