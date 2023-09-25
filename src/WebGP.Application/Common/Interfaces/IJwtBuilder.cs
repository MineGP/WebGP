namespace WebGP.Application.Common.Interfaces;
public interface IJwtBuilder
{
    IJwtBuilder AddRole(string role);
    IJwtBuilder AddId(string id);
    string Build(TimeSpan lifeTime);
}