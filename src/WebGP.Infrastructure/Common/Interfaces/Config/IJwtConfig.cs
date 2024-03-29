﻿using Microsoft.IdentityModel.Tokens;

namespace WebGP.Interfaces.Config;

public interface IJwtConfig
{
    string Issuer { get; }
    string Audience { get; }
    string Key { get; }

    SymmetricSecurityKey GetSecurityKey();
}