using System;
using System.Collections.Generic;

namespace WebGP.Domain.Entities;

public partial class OnlineLog
{
    public int Id { get; set; }

    public DateOnly Day { get; set; }

    public int Sec { get; set; }
}
