using MySqlConnector;
using WebGP.Interfaces.Config;

namespace WebGP.Infrastructure.Common.Configs;

public class DBConfig : IDBConfig
{
    public string User { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string DataBase { get; set; } = null!;
    public string Server { get; set; } = null!;
    public uint Port { get; set; }

    public string GetConnectionString() => new MySqlConnectionStringBuilder
    {
        Server = Server,
        Port = Port,
        Database = DataBase,
        UserID = User,
        Password = Password,
        ConnectionTimeout = 60,
        Pooling = false
    }.ConnectionString;
    
    public string GetConnectionString(string database) => new MySqlConnectionStringBuilder
    {
        Server = Server,
        Port = Port,
        Database = database,
        UserID = User,
        Password = Password,
        ConnectionTimeout = 60,
        Pooling = false
    }.ConnectionString;
}