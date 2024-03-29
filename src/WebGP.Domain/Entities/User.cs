﻿namespace WebGP.Domain.Entities;

public class User
{
    public int Id { get; set; }

    public string Uuid { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public bool? Male { get; set; }

    public string BirthdayDate { get; set; } = null!;

    public int? Phone { get; set; }

    public int? CardId { get; set; }

    public int Role { get; set; }

    public int? Work { get; set; }

    public DateTime? WorkTime { get; set; }

    public DateTime? RoleTime { get; set; }

    public int PhoneRegen { get; set; }

    public int CardRegen { get; set; }

    public int Wanted { get; set; }

    public int WantedId { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime ConnectDate { get; set; }

    public int Exp { get; set; }

    public DateTime LastUpdate { get; set; }
}