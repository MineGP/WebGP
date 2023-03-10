using MySql.Data.MySqlClient;
using WebGP.Interfaces.Config;

namespace WebGP.Models.Config;

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
        Password = Password
    }.ConnectionString;
}