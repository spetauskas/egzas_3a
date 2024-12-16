using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace egzas_3.Dtos.Requests
{
    /// <summary>
    /// User account login request
    /// </summary>
    public class LoginRequestDto
    {
        /// <summary>
        /// Username of the account
        /// </summary>
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string? AccountName { get; set; }
        /// <summary>
        /// Password of the account
        /// </summary>
        /// 



        //atblokuoti
        //[PasswordValidator]
        public string? AccountPassword { get; set; }
    }
}
