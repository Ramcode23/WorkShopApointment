using System.ComponentModel.DataAnnotations;

namespace AppoimentsWorkShop.DTOs
{
    public class UserLogins
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
