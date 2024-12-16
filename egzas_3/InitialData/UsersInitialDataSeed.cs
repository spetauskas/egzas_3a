using egzas_3.Entities;
using Microsoft.EntityFrameworkCore;

namespace egzas_3.InitialData
{
    public class UsersInitialDataSeed
    {
        public static List<User> Users => new()
        {
            new User
            {
                Id = Guid.NewGuid(),
                UserName = "John",
                UserSurName = "Doe",
                UserCountry = "USA",
                UserIdentityNumber = 12345,
                UserEmail = "john.doe@example.com",
                UserResidenceCity = "New York",
                UserResidenceStreet = "Broadway",
                UserResidenceHouseNumber = "123",
                 //AccountId = Guid.Parse("00000000-0000-0000-0000-000000000001"),

            },
             new User
            {
                Id = Guid.NewGuid(),
                UserName = "Jane",
                UserSurName = "Smith",
                UserCountry = "Canada",
                UserIdentityNumber = 54321,
                UserEmail = "jane.smith@example.com",
                UserResidenceCity = "Toronto",
                UserResidenceStreet = "Queen Street",
                UserResidenceHouseNumber = "456",
                //AccountId = Guid.Parse("00000000-0000-0000-0000-000000000002"),

            },
            new User
            {
                Id = Guid.NewGuid(),
                UserName = "Alice",
                UserSurName = "Johnson",
                UserCountry = "Australia",
                UserIdentityNumber = 98765,
                UserEmail = "alice.johnson@example.com",
                UserResidenceCity = "Sydney",
                UserResidenceStreet = "George Street",
                UserResidenceHouseNumber = "789",
                //AccountId = Guid.Parse("00000000-0000-0000-0000-000000000003"),

            },
        };
    }
}
