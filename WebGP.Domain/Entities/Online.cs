namespace WebGP.Domain.Entities;

public class Online
{
    public string Uuid { get; set; } = null!;

    public double X { get; set; }

    public double Y { get; set; }

    public double Z { get; set; }

    public int World { get; set; }

    public int? TimedId { get; set; }

    public string? DataIcon { get; set; }

    public string? DataName { get; set; }

    public bool IsOp { get; set; }

    public string? ZoneSelector { get; set; }

    public string? SkinUrl { get; set; }

    public bool Die { get; set; }

    public string Gpose { get; set; } = null!;

    public bool Hide { get; set; }

    public DateTime LastUpdate { get; set; }
}