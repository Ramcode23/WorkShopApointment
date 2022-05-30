using AppoimentsWorkShop.DTOs.Appointment;
using AppoimentsWorkShop.Models;
using AutoMapper;

namespace AppoimentsWorkShop.Utitlities
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AppointmentCreateDTO, Appointment>();
            CreateMap<AppointmentCreateDTO, Appointment>().ReverseMap();
            CreateMap<AppointmentDTO, Appointment>();
            CreateMap<AppointmentDTO, Appointment>().ReverseMap();
        }
    }
}
