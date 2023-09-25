namespace WebGP.Interfaces.Config;

public interface IDBConfig
{
    string User { get; }
    string Password { get; }
    string DataBase { get; }
    string Server { get; }
    uint Port { get; }

    string GetConnectionString();
}