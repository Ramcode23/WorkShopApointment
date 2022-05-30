using AppoimentsWorkShop.DataAccess;
using AppoimentsWorkShop.Models;
using AppoimentsWorkShop.Utitlities;
using Microsoft.EntityFrameworkCore;

namespace AppoimentsWorkShop.Services
{
    public class AppointmentsService: IAppointmentsService
    {
        private readonly DataContext _context;
        public AppointmentsService(DataContext context)
        {
            _context = context;
        
       }

        public Task AddAsync(Appointment entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            _context.Appointments.Add(entity);
          return   _context.SaveChangesAsync();
        }

    

        public bool Exists(int Id)
        {
            return _context.Appointments.Any(x => x.Id == Id);
        }

        public IQueryable<Appointment> GetAllAsync(int pageNumber, int resultsPage)
        {
         var apointments=    _context.Appointments
                .Include(x => x.Client)
                .Include(x => x.WorkShop)
                .Where(x => x.WorkShop ==null)
                .OrderBy(x=>x.Client.Email)
                .ThenBy(x=>x.CreatedAt).AsQueryable();
            return Paginator.GetPage(apointments, pageNumber, resultsPage);
        }

        public Task<Appointment?> GetByIdAsync(int id)
        {
            return _context.Appointments.Where(a => a.Id == id).FirstOrDefaultAsync();
        }

        public Task UpdateAsync(Appointment entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            _context.Entry(entity).State = EntityState.Modified;
            return  _context.SaveChangesAsync();
        }
    }
}
