using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppoimentsWorkShop.DataAccess;
using AppoimentsWorkShop.Models;
using AppoimentsWorkShop.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using AppoimentsWorkShop.DTOs.Appointment;
using AutoMapper;
using AppoimentsWorkShop.Helpers;

namespace AppoimentsWorkShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
       
        private readonly IAppointmentsService _appointmentsService;
        private readonly IMapper _mapper;
        private readonly IUserHelper _userHelper;
        public AppointmentsController(
              IAppointmentsService appointmentsService,
              IMapper mapper,
              IUserHelper userHelper
            )
        {
            _mapper = mapper;
            _appointmentsService =appointmentsService;
            _userHelper = userHelper;
        }

        // GET: api/Appointments
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "workshop")]
        public async Task<ActionResult<IEnumerable<AppointmentDTO>>> GetAppointments([FromQuery] int pageNumber, int resultsPage)
        {
       var appointments=  await Task.FromResult(_appointmentsService.GetAllAsync(pageNumber, resultsPage).ToList());
          if (appointments.Any())
          {
                var apoimentlis= _mapper.Map<List<AppointmentDTO>>(appointments);
                return apoimentlis;


          }
            return new   List<AppointmentDTO>();
        }

     

        // PUT: api/Appointments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("confirmappointment/{id}")]
    
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "workshop")]
        public async Task<IActionResult> PutAppointment(  int id, AppoimentState appoimentState)
        {

            var appointment = await _appointmentsService.GetByIdAsync(id);

            if (appointment == null)
            {
                return BadRequest();
            }

            try
            {
                var user = await _userHelper.GetUserByEmailAsync(User.Identity.Name);
                appointment.AppoimentState=appoimentState;
                appointment.WorkShop = user;
            await _appointmentsService.UpdateAsync(appointment);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointmentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }


            return Ok(new
            {
                msj = "Your Appointment has been Updated"
            });
        }

        // POST: api/Appointments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("makeappointment")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "client")]
        public async Task<ActionResult> PostAppointment( [ FromBody] AppointmentCreateDTO appointmentDTO)
        {
          if (appointmentDTO == null)
          {
              return Problem("Entity set 'DataContext.Appointments'  is null.");
          }
            var user = await _userHelper.GetUserByEmailAsync(User.Identity.Name);
            var appointment= _mapper.Map<Appointment>(appointmentDTO);
            appointment.Client = user;
         
            await _appointmentsService.AddAsync(appointment);

            return Ok(new
            {
                msj = "Your Appointment has been created"
            });
        }

       

        private bool AppointmentExists(int id)
        {
            return _appointmentsService.Exists(id);
        }
    }
}
