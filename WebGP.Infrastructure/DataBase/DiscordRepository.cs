/*using System.Data;
using Dapper;
using MySql.Data.MySqlClient;
using WebGP.Application.Common.Interfaces;
using WebGP.Application.Common.VM;

namespace WebGP.Infrastructure.DataBase;

public class DiscordRepository : IDiscordRepository
{
    private const string SELECT_QUERY = @"
            SELECT
	            discord.discord_id AS 'discord_id',
	            GetLevel(users.exp) AS 'level',
	            r.name AS 'role',
	            w.name AS 'work',
                users.phone AS 'phone'
            FROM users
            INNER JOIN discord ON discord.uuid = users.uuid
            LEFT JOIN role_work_readonly r ON users.`work` = r.id AND r.`type` = 'WORK'
            LEFT JOIN role_work_readonly w ON users.`role` = w.id AND w.`type` = 'ROLE'";

    private readonly IDbConnection _connection;

    public DiscordRepository(string connectionString)
    {
        _connection = new MySqlConnection(connectionString);
    }

    public Task<IEnumerable<DiscordVM>> GetDiscordListAsync()
    {
        return _connection.QueryAsync<DiscordVM>(SELECT_QUERY);
    }
}*/