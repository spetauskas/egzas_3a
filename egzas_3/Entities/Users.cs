using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace egzas_3.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        // Add [Required] attributes to ensure non-null fields
        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public string UserSurName { get; set; } = string.Empty;

        [Required]
        public string UserCountry { get; set; } = string.Empty;

        [Required]
        public int UserIdentityNumber { get; set; }

        [Required]
        [EmailAddress] // Add email validation
        public string UserEmail { get; set; } = string.Empty;

        [Required]
        public string UserResidenceCity { get; set; } = string.Empty;

        [Required]
        public string UserResidenceStreet { get; set; } = string.Empty;

        [Required]
        public string UserResidenceHouseNumber { get; set; } = string.Empty;

        // Optional apartment number remains nullable
        public string? UserResidenceApartmentNumber { get; set; }

        //[ForeignKey(nameof(Account))]
        //public Guid AccountId { get; set; }

        // Navigation property
        //public Account Account { get; set; } = null!;
    }
}
