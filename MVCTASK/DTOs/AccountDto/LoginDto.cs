using System.ComponentModel.DataAnnotations;

namespace MVCTASK.DTOs.AccountDto
{
    public class LoginDto
    {
        [Required]
        public string UserNameOrEmail {  get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        public bool IsRemembered {  get; set; }
    }
}
