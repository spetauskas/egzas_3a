//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;


//namespace egzas_3.Entities {
//    public class Account
//    {

//        public Guid AccountId { get; set; }


//        [StringLength(50, MinimumLength = 3)] // Add length constraints
//        public string AccountName { get; set; } = string.Empty;


//        public byte[] AccountPasswordHash { get; set; } = null!;


//        public byte[] AccountPasswordSalt { get; set; } = null!;


//        [StringLength(20)] // Limit role name length
//        public string AccountRole { get; set; } = "user";

//        // Add navigation property
//        //public User? User { get; set; }
//    }
//}
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace egzas_3.Entities
{
    public class Account
    {
        //[Key]
        public Guid AccountId { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Account name must be between 3 and 50 characters")]
        public string AccountName { get; set; } = string.Empty;
        [Required]
        public string AccountEmail { get; set; } = string.Empty;

        [Required]
        public byte[] AccountPasswordHash { get; set; } = null!;

        [Required]
        public byte[] AccountPasswordSalt { get; set; } = null!;

        [StringLength(20, ErrorMessage = "Account role must be 20 characters or less")]
        public string AccountRole { get; set; } = "user";
    }
}
