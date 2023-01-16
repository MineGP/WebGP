using Microsoft.EntityFrameworkCore;
using WebGP.Models.DataBase.Self;

namespace WebGP.Interfaces.DataBase.Self
{
    public interface ISqlContext
    {
        DbSet<ClientAPI> Clients { get; }
    }
}
