using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppoimentsWorkShop.Models
{
    public class Appointment
    {
        [Required]
        [Key]
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }=String.Empty;
        [Required]
        public string Message { get; set; } = String.Empty;
        public DateTime AppointmentDate { get; set; } = DateTime.Now;
        public User Client { get; set; } = new User();
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public User? WorkShop { get; set; } 
     
        public DateTime? UpdatedAt { get; set; }

        public AppoimentState AppoimentState { get; set; } = AppoimentState.Confirm;


    }
}
