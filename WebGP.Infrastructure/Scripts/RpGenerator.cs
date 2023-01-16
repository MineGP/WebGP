using Microsoft.Extensions.Options;
using WebGP.Application.Common.Interfaces;

namespace WebGP.Infrastructure.Scripts;

public class RpGenerator : IRpGenerator
{
    private Config _config;

    public RpGenerator(IConfigureOptions<Config> options)
    {
        options.Configure(_config = new Config());
    }

    public Task<string> GenerateAsync(Uri url, IEnumerable<string> executable)
    {
        throw new NotImplementedException();
    }

    public class Config
    {
        public string RunCommand { get; set; } = "echo WTF!";
    }
}