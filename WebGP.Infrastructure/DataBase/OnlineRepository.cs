using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Query;
using MySql.Data.MySqlClient;
using System.Data;
using WebGP.Application.Common.Interfaces;
using WebGP.Application.Common.VM;
using WebGP.Domain.Entities;

namespace WebGP.Infrastructure.DataBase
{
    public class OnlineRepository : IOnlineRepository
    {
        private readonly IDbConnection _connection;
        private const string SELECT_ONLINE_QUERY = @"
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
            LEFT JOIN role_work_readonly w ON users.`role` = w.id AND w.`type` = 'ROLE'";

        private const string SELECT_ONLINE_LOG_QUERY = @"
            SELECT
                online_logs.day,
                online_logs.sec
            FROM online_logs
            INNER JOIN users ON users.id = online_logs.id
            WHERE {0}
                AND online_logs.day >= @from
                AND online_logs.day <= @to";

        public OnlineRepository(string connectionString)
        {
            _connection = new MySqlConnection(connectionString);
        }

        public Task<IEnumerable<OnlineVM>> GetOnlineListAsync()
        {
            return _connection.QueryAsync<OnlineVM>(SELECT_ONLINE_QUERY);
        }

        public Task<IEnumerable<OnlineLog>> GetOnlineLogListByUserNameAsync(string userName, DateTime from, DateTime to)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("from", from);
            parameters.Add("to", to);
            parameters.Add("value", userName);
            return _connection.QueryAsync<OnlineLog>(new CommandDefinition(SELECT_ONLINE_LOG_QUERY.Replace("{0}", "users.user_name = @value"), parameters));
        }
        public Task<IEnumerable<OnlineLog>> GetOnlineLogListByUuidAsync(string uuid, DateTime from, DateTime to)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("from", from);
            parameters.Add("to", to);
            parameters.Add("value", uuid);
            return _connection.QueryAsync<OnlineLog>(new CommandDefinition(SELECT_ONLINE_LOG_QUERY.Replace("{0}", "users.uuid = @value"), parameters));
        }
        public Task<IEnumerable<OnlineLog>> GetOnlineLogListByStaticIDAsync(uint staticID, DateTime from, DateTime to)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("from", from);
            parameters.Add("to", to);
            parameters.Add("value", staticID);
            return _connection.QueryAsync<OnlineLog>(new CommandDefinition(SELECT_ONLINE_LOG_QUERY.Replace("{0}", "users.id = @value"), parameters));
        }

        public Task<int> GetOnlineCountAsync()
        {
            return _connection.QuerySingleAsync<int>("SELECT COUNT(1) FROM online");
        }
    }
}
