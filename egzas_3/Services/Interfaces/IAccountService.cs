﻿namespace egzas_3.Services.Interfaces
{
    public interface IAccountService
    {
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);

    }
}
