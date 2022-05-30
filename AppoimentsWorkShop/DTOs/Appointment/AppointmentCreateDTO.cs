using System.ComponentModel.DataAnnotations;

namespace AppoimentsWorkShop.DTOs.Appointment
{
    public class AppointmentCreateDTO
    {
        [Required]
        public string Decripcion { get; set; } = String.Empty;

        [Required]
        public string Message { get; set; } = String.Empty;

        [Required]
        public DateTime AppointmentDate { get; set; } = DateTime.Now;
    }
}
