using System;
using System.Collections.Generic;

namespace WebGP.Domain.Entities;

public partial class Discord
{
    public long DiscordId { get; set; }

    public string Uuid { get; set; } = null!;

    public DateTime LastUpdate { get; set; }
}
