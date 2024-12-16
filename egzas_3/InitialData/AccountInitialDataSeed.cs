//using System.Security.Cryptography;
//using System.Text;

//namespace egzas_3.InitialData
//{
//    public static class AccountInitialDataSeed
//    {
//        public static List<Account> Accounts => new()
//        {
//            user1(),

//        };

//        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
//        {
//            using var hmac = new HMACSHA256();
//            passwordSalt = hmac.Key;
//            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
//        }
//        private static Account user1()
//        {
//            CreatePasswordHash("user1", out var passwordHash, out var passwordSalt);
//            return new Account
//            {
//                AccountId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
//                AccountName = "user1",
//                AccountPasswordHash = passwordHash,
//                AccountPasswordSalt = passwordSalt,
//                AccountRole = "admin",
//            };
//        }


//    }
//}

using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using egzas_3.Entities;

namespace egzas_3.InitialData
{
    public static class AccountInitialDataSeed
    {
        public static List<Account> Accounts => new()
        {
            CreateAdminUser()
        };

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA256();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        private static Account CreateAdminUser()
        {
            CreatePasswordHash("AdminPassword123!", out var passwordHash, out var passwordSalt);
            return new Account
            {
                AccountId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                AccountName = "admin",
                AccountEmail = "spetauskas@gmail.com",
                AccountPasswordHash = passwordHash,
                AccountPasswordSalt = passwordSalt,
                AccountRole = "admin"
            };
        }
    }
}
