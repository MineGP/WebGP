using System;
using System.Collections.Generic;

namespace WebGP.Domain.Entities;

public partial class Online
{
    public string Uuid { get; set; } = null!;

    public double X { get; set; }

    public double Y { get; set; }

    public double Z { get; set; }

    public int World { get; set; }

    public int? TimedId { get; set; }

    public bool IsOp { get; set; }

    public string? SkinUrl { get; set; }
}
