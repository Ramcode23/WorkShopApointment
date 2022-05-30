using AppoimentsWorkShop.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AppoimentsWorkShop.DataAccess
{
    public class DataContext: IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<User> Users { get; set; }


    }
}
