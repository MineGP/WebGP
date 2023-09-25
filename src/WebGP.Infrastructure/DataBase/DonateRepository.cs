using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using WebGP.Application.Common.Interfaces;

namespace WebGP.Infrastructure.DataBase;

public class DonateRepository : IDonateRepository
{
    private readonly DonateDbContext _context;

    public DonateRepository(DonateDbContext context)
    {
        _context = context;
    }

    private const string OnUpdatePreDonateQuery = @"SELECT OnUpdatePreDonate()";
    private const string InsertPreDonateQuery = @"
       INSERT
            INTO predonate (predonate.name, predonate.amount, predonate.uuid, predonate.type)
            VALUES (@name, @amount, @uuid, @type)";

    public async Task AddPreDonateAsync(string name, string? uuid, int amount, string type, CancellationToken cancellationToken)
        => await _context.Database
            .ExecuteSqlRawAsync(InsertPreDonateQuery, new object[] {
                new MySqlParameter("name", name),
                new MySqlParameter("uuid", uuid),
                new MySqlParameter("amount", amount),
                new MySqlParameter("type", type)
             }, cancellationToken);

    public async Task OnUpdatePreDonateAsync(CancellationToken cancellationToken)
        => await _context.Database
            .ExecuteSqlRawAsync(OnUpdatePreDonateQuery, cancellationToken);
}
