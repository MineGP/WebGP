namespace WebGP.Application.Common.Interfaces;

public interface IRpGenerator
{
    Task<string> GenerateAsync(Uri url, IEnumerable<string> executable);
}