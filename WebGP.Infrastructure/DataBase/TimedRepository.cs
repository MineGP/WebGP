using Dapper;
using MySql.Data.MySqlClient;
using System.Data;
using WebGP.Application.Common.Interfaces;
using WebGP.Application.Common.VM;

namespace WebGP.Infrastructure.DataBase
{
    public class TimedRepository : ITimedRepository
    {
        private readonly IDbConnection _connection;
        private const string SELECT_QUERY = @"
SELECT
	online.timed_id AS 'timed_id',
	online.`uuid` AS 'uuid',
	users.first_name AS 'first_name',
	users.last_name AS 'last_name',
	discord.discord_id AS 'discord_id',
	r.name AS 'role',
	w.name AS 'work',
	GetLevel(IFNULL(users.exp,0)) AS 'level',
    online.skin_url AS 'skin_url'
FROM online
LEFT JOIN users ON users.uuid = online.uuid
LEFT JOIN discord ON discord.uuid = online.uuid
LEFT JOIN role_work_readonly r ON users.`work` = r.id AND r.`type` = 'WORK'
LEFT JOIN role_work_readonly w ON users.`role` = w.id AND w.`type` = 'ROLE'
";

        public TimedRepository(string connectionString)
        {
            _connection = new MySqlConnection(connectionString);
        }

        public async Task<IEnumerable<TimedVM>> GetTimedAsync()
        {
            return await _connection.QueryAsync<TimedVM>(SELECT_QUERY);
        }
    }
}
