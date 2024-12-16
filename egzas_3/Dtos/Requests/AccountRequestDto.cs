using System.ComponentModel.DataAnnotations;


namespace egzas_3.Dtos.Requests
{
    public record AccountRequestDto
    {
        /// <summary>
        /// Username of the account
        /// </summary>
        [Required]
        [StringLength(50, MinimumLength = 0)]
        public string? AccountName { get; set; }

        /// <summary>
        /// Password of the account
        /// </summary>
        //[PasswordValidator]
        public string? AccountPassword { get; set; }

        /// <summary>
        /// Email of the account
        /// </summary>
        [EmailAddress]
        //[EmailDomainValidator]
        public string? AccountEmail { get; set; }

        /// <summary>
        /// Role of the account
        /// </summary>
        //[RoleValidator]
        public string? AccountRole { get; set; }
    }



}
