using WebGP.Interfaces.Auth;

namespace WebGP.Models.Auth;

public record BearerToken(string Token) : IBearerToken;