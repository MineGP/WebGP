using System;
using System.Collections.Generic;

namespace WebGP.Domain.Entities;

public partial class User
{
    public int Id { get; set; }

    public string Uuid { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public bool? Male { get; set; }

    public DateTime BirthdayDate { get; set; }

    public int? Phone { get; set; }

    public int? CardId { get; set; }

    public int Role { get; set; }

    public int? Work { get; set; }

    public DateTime? WorkTime { get; set; }

    public DateTime? RoleTime { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime ConnectDate { get; set; }

    public int Exp { get; set; }

    public DateTime LastUpdate { get; set; }
}
