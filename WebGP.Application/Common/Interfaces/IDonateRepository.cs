namespace WebGP.Application.Common.Interfaces;

public interface IDonateRepository
{
    Task OnUpdatePreDonateAsync(CancellationToken cancellationToken);
    Task AddPreDonateAsync(string name, string? uuid, int amount, string type, CancellationToken cancellationToken);
}