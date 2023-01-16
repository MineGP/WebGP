using MySql.Data.MySqlClient;
using WebGP.Interfaces.Config;

namespace WebGP.Models.Config;

public class DBConfig : IDBConfig
{
    public string User { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string DataBase { get; set; } = null!;
    public uint Port { get; set; }

    public string GetConnectionString()
    {
        return new MySqlConnectionStringBuilder
        {
            Server = "127.0.0.1",
            Port = Port,
            Database = DataBase,
            UserID = User,
            Password = Password
        }.ConnectionString;
    }
}