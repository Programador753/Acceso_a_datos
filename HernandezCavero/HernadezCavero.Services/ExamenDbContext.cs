using Microsoft.EntityFrameworkCore;

namespace HernadezCavero.Services
{
    public class ExamenDbContext : DbContext
    {
        public ExamenDbContext(DbContextOptions<ExamenDbContext> options) : base(options)
        {
        }

    }
}
