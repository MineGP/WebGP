﻿namespace WebGP.Domain.Entities;

public class RoleWorkReadonly
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;

    public string? Icon { get; set; }

    public string Name { get; set; } = null!;
}