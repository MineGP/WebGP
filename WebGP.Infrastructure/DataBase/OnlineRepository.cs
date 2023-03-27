using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using WebGP.Application.Common.Interfaces;
using WebGP.Application.Common.VM;
using WebGP.Domain.Entities;

namespace WebGP.Infrastructure.DataBase;

public class OnlineRepository : IOnlineRepository
{
    private readonly ApplicationDbContext _context;

    private const string SelectOnlineQuery = @"
            SELECT
	            online.timed_id AS 'TimedId',
	            online.`uuid` AS 'Uuid',
                users.id AS 'StaticId',
	            users.first_name AS 'FirstName',
	            users.last_name AS 'LastName',
	            discord.discord_id AS 'DiscordId',
	            r.name AS 'Role',
	            w.name AS 'Work',
	            GetLevel(IFNULL(users.exp,0)) AS 'Level',
                online.skin_url AS 'SkinUrl'
            FROM online
            LEFT JOIN users ON users.uuid = online.uuid
            LEFT JOIN discord ON discord.uuid = online.uuid
            LEFT JOIN role_work_readonly r ON users.`work` = r.id AND r.`type` = 'WORK'
            LEFT JOIN role_work_readonly w ON users.`role` = w.id AND w.`type` = 'ROLE'";

    private const string WhereTimedId = @"
            WHERE online.timed_id = @timed_id";    
    
    private const string WhereUuid = @"
            WHERE online.`uuid` = @uuid";

    private const string WhereStaticId = @"
            WHERE users.id = @id";

    public OnlineRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<OnlineVm?> GetOnlineByTimedIdAsync(int timedId, CancellationToken cancellationToken)
    {
        var query = SelectOnlineQuery + WhereTimedId + ';';
        return await _context.Database
            .SqlQueryRaw<OnlineVm>(query, new SqlParameter("timed_id", timedId))
            .SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<IDictionary<int, OnlineVm>> GetAllOnlineByTimedIdAsync(CancellationToken cancellationToken)
    {
        var query = SelectOnlineQuery + ';';

        return await _context.Database
            .SqlQueryRaw<OnlineVm>(query)
            .ToDictionaryAsync(v => v.TimedId, v => v, cancellationToken);
    }

    public async Task<OnlineVm?> GetOnlineByUuidAsync(string uuid, CancellationToken cancellationToken)
    {
        var query = SelectOnlineQuery + WhereUuid + ';';

        return await _context.Database
            .SqlQueryRaw<OnlineVm>(query, new SqlParameter("uuid", uuid))
            .SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<IDictionary<string, OnlineVm>> GetAllOnlineByUuidAsync(CancellationToken cancellationToken)
    {
        var query = SelectOnlineQuery + ';';

        return await _context.Database
            .SqlQueryRaw<OnlineVm>(query)
            .ToDictionaryAsync(v => v.Uuid, v => v, cancellationToken);
    }

    public async Task<OnlineVm?> GetOnlineByStaticIdAsync(uint staticId, CancellationToken cancellationToken)
    {
        var query = SelectOnlineQuery + WhereStaticId + ';';

        return await _context.Database
            .SqlQueryRaw<OnlineVm>(query, new SqlParameter("id", staticId))
            .SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<IDictionary<uint, OnlineVm>> GetAllOnlineByStaticIdAsync(CancellationToken cancellationToken)
    {
        var query = WhereStaticId + ';';

        return await _context.Database
            .SqlQueryRaw<OnlineVm>(query)
            .ToDictionaryAsync(v => v.StaticId, v => v, cancellationToken);
    }

    public async Task<int> GetOnlineCountAsync(CancellationToken cancellationToken)
    {
        return await _context.Onlines.CountAsync(cancellationToken);
    }
}