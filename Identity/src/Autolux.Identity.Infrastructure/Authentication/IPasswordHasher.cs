﻿namespace Autolux.Identity.Infrastructure.Authentication;
public interface IPasswordHasher
{
    string Hash(string password);

    bool Verify(string password, string passwordHash);
}
