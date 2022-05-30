using AppoimentsWorkShop.Models;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AppoimentsWorkShop.DTOs.Appointment
{
    public class AppointmentDTO
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Decripcion { get; set; } = String.Empty;
        [Required]
        public string Message { get; set; } = String.Empty;

        public DateTime AppointmentDate { get; set; }

        [JsonProperty("ClientEmail")]
        public string ClientUserName { get; set; }

        [JsonProperty("ClientFirstName")]
        public  string  ClientFirstName { get; set; }
        [JsonProperty("ClientLastName")]
        public string ClientLastName { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [JsonProperty("WorkshopUser")]
        public string? WorkShopUserName { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public AppoimentState AppoimentState { get; set; } 
    }
}
