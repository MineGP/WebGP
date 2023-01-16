using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebGP.Application.Common.Interfaces
{
    public interface IRpGenerator
    {
        Task<string> GenerateAsync(Uri url, IEnumerable<string> executable);
    }
}
