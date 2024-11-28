using CarRentalSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalSystem.Data
{
    public class DbContextcs : DbContext
    {
        public DbContextcs(DbContextOptions<DbContextcs> options) : base(options)
        {

        }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<CarModel> Cars { get; set; }
    }
}
