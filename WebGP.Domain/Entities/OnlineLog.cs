using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebGP.Domain.Entities
{
    public class OnlineLog
    {
        [JsonPropertyName("day")] public DateTime Day { get; set; }
        [JsonPropertyName("sec")] public int Seconds { get; set; }
    }
}
