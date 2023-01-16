using System;
using System.Collections.Generic;

namespace WebGP.Domain.Entities;

public partial class RoleWorkReadonly
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;

    public string? Icon { get; set; }

    public string Name { get; set; } = null!;
}
