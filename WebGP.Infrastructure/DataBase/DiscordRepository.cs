using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using WebGP.Application.Common.Interfaces;
using WebGP.Application.Common.VM;

namespace WebGP.Infrastructure.DataBase;

public class DiscordRepository : IDiscordRepository
{
    private readonly IContextGPO _contextGPO;
    private readonly IContextGPC _contextGPC;

    private const string SelectQuery = @"
            SELECT
	            discord.discord_id AS 'DiscordId',
	            GetLevel(users.exp) AS 'Level',
	            r.name AS 'Role',
	            w.name AS 'Work',
                users.phone AS 'Phone',
                users.id AS 'StaticId',
                users.first_name AS 'FirstName',
	            users.last_name AS 'LastName',
                users.uuid AS 'Uuid'
            FROM users
            INNER JOIN discord ON discord.uuid = users.uuid
            LEFT JOIN role_work_readonly w ON users.`work` = w.id AND w.`type` = 'WORK'
            LEFT JOIN role_work_readonly r ON users.`role` = r.id AND r.`type` = 'ROLE'";

    private const string WhereDiscordId = @"
            WHERE discord.discord_id = @discord_id";

    public DiscordRepository(IContextGPO contextGPO, IContextGPC contextGPC)
    {
        _contextGPO = contextGPO;
        _contextGPC = contextGPC;
        Console.WriteLine("GPO: " + contextGPO.DbContext.Database.GetConnectionString());
        Console.WriteLine("GPC: " + contextGPC.DbContext.Database.GetConnectionString());
    }

    private IContext GetBy(ContextType database) => database switch
    {
        ContextType.GPO => _contextGPO,
        ContextType.GPC => _contextGPC,
        _ => throw new NotSupportedException($"ContextType '{database}' not support")
    };

    public async Task<IDictionary<long, DiscordVm>> GetAllDiscordsAsync(ContextType database, CancellationToken cancellationToken)
    {
        return await GetBy(database).DbContext.Database
            .SqlQueryRaw<DiscordVm>(SelectQuery)
            .ToDictionaryAsync(
                v => v.DiscordId,
                v => v,
                cancellationToken);
    }

    public async Task<DiscordVm?> GetByDiscordIdAsync(ContextType database, long discordId, CancellationToken cancellationToken)
    {
        return await GetBy(database).DbContext.Database
            .SqlQueryRaw<DiscordVm>(SelectQuery + WhereDiscordId, new MySqlParameter("discord_id", discordId))
            .SingleOrDefaultAsync(cancellationToken);
    }
}
