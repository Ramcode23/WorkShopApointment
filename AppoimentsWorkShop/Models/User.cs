using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppoimentsWorkShop.Models
{
    public class User: IdentityUser
    {

        [Required]
        [Key]
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;


        [InverseProperty("Client")]
        public ICollection<Appointment> AppointmentsClient { get; set; } = new List<Appointment>();
        [InverseProperty("WorkShop")]
        public ICollection<Appointment> AppointmentsWorkShop { get; set; } = new List<Appointment>();


    }
    
}
