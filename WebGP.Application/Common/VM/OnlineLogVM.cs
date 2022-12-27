using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WebGP.Domain.Entities;

namespace WebGP.Application.Common.VM
{
    public class OnlineLogVM
    {
        [JsonPropertyName("day")] public string Day { get; set; } = null!;
        [JsonPropertyName("sec")] public int Seconds { get; set; }
    }
}
