using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using MySqlConnector;
using WebGP.Application.Common.Interfaces;
using WebGP.Application.Common.VM;

namespace WebGP.Infrastructure.DataBase;

public class OnlineRepository : IOnlineRepository
{
    private readonly IContextGPO _contextGPO;
    private readonly IContextGPC _contextGPC;

    private static string CreateSelectOnlineQuery(bool hasRace) => $@"
            SELECT
	            online.timed_id AS 'TimedId',
	            online.`uuid` AS 'Uuid',
                users.id AS 'StaticId',
	            users.first_name AS 'FirstName',
	            users.last_name AS 'LastName',
	            discord.discord_id AS 'DiscordId',
	            r.name AS 'Role',
	            w.name AS 'Work',
                {(hasRace ? "race_types.race" : "NULL")} AS 'Race',
	            GetLevel(IFNULL(users.exp,0)) AS 'Level',
                online.skin_url AS 'SkinUrl'
            FROM online
            LEFT JOIN users ON users.uuid = online.uuid
            LEFT JOIN discord ON discord.uuid = online.uuid
            LEFT JOIN role_work_readonly w ON users.`work` = w.id AND w.`type` = 'WORK'
            LEFT JOIN role_work_readonly r ON users.`role` = r.id AND r.`type` = 'ROLE'
            {(hasRace ? "LEFT JOIN race_types ON race_types.id = users.race_id" : "")}";

    private static readonly string SelectOnlineQueryGPO = CreateSelectOnlineQuery(false);

    private static readonly string SelectOnlineQueryGPC = CreateSelectOnlineQuery(true);

    private const string WhereTimedId = @"
            WHERE online.timed_id = @timed_id";    
    
    private const string WhereUuid = @"
            WHERE online.`uuid` = @uuid";

    private const string WhereStaticId = @"
            WHERE users.id = @id";

    public OnlineRepository(IContextGPO contextGPO, IContextGPC contextGPC)
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
        ContextType.GPO => SelectOnlineQueryGPO,
        ContextType.GPC => SelectOnlineQueryGPC,
        _ => throw new NotSupportedException($"ContextType '{database}' not support")
    };

    public async Task<OnlineVm?> GetOnlineByTimedIdAsync(ContextType database, int timedId, CancellationToken cancellationToken)
    {
        return await GetBy(database).DbContext.Database
            .SqlQueryRaw<OnlineVm>(GetSelectBy(database) + WhereTimedId, new MySqlParameter("timed_id", timedId))
            .SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<IDictionary<int, OnlineVm>> GetAllOnlineByTimedIdAsync(ContextType database, CancellationToken cancellationToken)
    {
        return await GetBy(database).DbContext.Database
            .SqlQueryRaw<OnlineVm>(GetSelectBy(database))
            .Where(v => v.TimedId.HasValue)
            .ToDictionaryAsync(v => v.TimedId!.Value, v => v, cancellationToken);
    }

    public async Task<OnlineVm?> GetOnlineByUuidAsync(ContextType database, string uuid, CancellationToken cancellationToken)
    {
        return await GetBy(database).DbContext.Database
            .SqlQueryRaw<OnlineVm>(GetSelectBy(database) + WhereUuid, new MySqlParameter("uuid", uuid))
            .SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<IDictionary<string, OnlineVm>> GetAllOnlineByUuidAsync(ContextType database, CancellationToken cancellationToken)
    {
        return await GetBy(database).DbContext.Database
            .SqlQueryRaw<OnlineVm>(GetSelectBy(database))
            .Where(v => v.Uuid != null)
            .ToDictionaryAsync(v => v.Uuid!, v => v, cancellationToken);
    }

    public async Task<OnlineVm?> GetOnlineByStaticIdAsync(ContextType database, uint staticId, CancellationToken cancellationToken)
    {
        return await GetBy(database).DbContext.Database
            .SqlQueryRaw<OnlineVm>(GetSelectBy(database) + WhereStaticId, new MySqlParameter("id", staticId))
            .SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<IDictionary<uint, OnlineVm>> GetAllOnlineByStaticIdAsync(ContextType database, CancellationToken cancellationToken)
    {
        return await GetBy(database).DbContext.Database
            .SqlQueryRaw<OnlineVm>(GetSelectBy(database))
            .Where(v => v.StaticId.HasValue)
            .ToDictionaryAsync(v => v.StaticId!.Value, v => v, cancellationToken);
    }

    public async Task<int> GetOnlineCountAsync(ContextType database, CancellationToken cancellationToken)
    {
        return await GetBy(database).Onlines.CountAsync(cancellationToken);
    }
}
