﻿namespace WebGP.Domain.Entities;

public class OnlineLog
{
    public int Id { get; set; }

    public DateOnly Day { get; set; }

    public int Sec { get; set; }
}