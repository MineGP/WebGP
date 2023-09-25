using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using WebGP.Application.Common.Interfaces;
using WebGP.Application.Common.VM;

namespace WebGP.Infrastructure.DataBase;

public class DiscordRepository : IDiscordRepository
{
    private readonly IContextGPO _contextGPO;
    private readonly IContextGPC _contextGPC;

    private static string CreateOnlineQuery(bool hasRace) => $@"
            SELECT
	            discord.discord_id AS 'DiscordId',
	            GetLevel(IFNULL(users.exp,0)) AS 'Level',
	            r.name AS 'Role',
	            w.name AS 'Work',
                {(hasRace ? "race_types.race" : "NULL")} AS 'Race',
                users.phone AS 'Phone',
                users.id AS 'StaticId',
                users.first_name AS 'FirstName',
	            users.last_name AS 'LastName',
                discord.uuid AS 'Uuid'
            FROM discord
            LEFT JOIN users ON discord.uuid = users.uuid
            LEFT JOIN role_work_readonly w ON users.`work` = w.id AND w.`type` = 'WORK'
            LEFT JOIN role_work_readonly r ON users.`role` = r.id AND r.`type` = 'ROLE'
            {(hasRace ? "LEFT JOIN race_types ON race_types.id = users.race_id" : "")}";

    private static readonly string SelectQueryGPO = CreateOnlineQuery(false);

    private static readonly string SelectQueryGPC = CreateOnlineQuery(true);

    private const string WhereDiscordId = @"
            WHERE discord.discord_id = @discord_id";

    public DiscordRepository(IContextGPO contextGPO, IContextGPC contextGPC)
    {
        _contextGPO = contextGPO;
        _contextGPC = contextGPC;
    }

    private IContext GetBy(ContextType database) => database switch
    {
        ContextType.GPO => _contextGPO,
        ContextType.GPC => _contextGPC,
        _ => throw new NotSupportedException($"ContextType '{database}' not support")
    };
    private string GetSelectBy(ContextType database) => database switch
    {
        ContextType.GPO => SelectQueryGPO,
        ContextType.GPC => SelectQueryGPC,
        _ => throw new NotSupportedException($"ContextType '{database}' not support")
    };

    public async Task<IDictionary<long, DiscordVm>> GetAllDiscordsAsync(ContextType database, CancellationToken cancellationToken)
    {
        return await GetBy(database).DbContext.Database
            .SqlQueryRaw<DiscordVm>(GetSelectBy(database))
            .ToDictionaryAsync(
                v => v.DiscordId,
                v => v,
                cancellationToken);
    }

    public async Task<DiscordVm?> GetByDiscordIdAsync(ContextType database, long discordId, CancellationToken cancellationToken)
    {
        return await GetBy(database).DbContext.Database
            .SqlQueryRaw<DiscordVm>(GetSelectBy(database) + WhereDiscordId, new MySqlParameter("discord_id", discordId))
            .SingleOrDefaultAsync(cancellationToken);
    }
}
