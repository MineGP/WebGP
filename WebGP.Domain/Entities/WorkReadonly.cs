namespace WebGP.Domain.Entities;

public partial class WorkReadonly
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;

    public string Icon { get; set; } = null!;

    public string Name { get; set; } = null!;
}
