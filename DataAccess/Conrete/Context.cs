using Microsoft.EntityFrameworkCore;
using tasinmazz.Entity.Conrete;

namespace tasinmazz.DataAccess.Conrete

{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        public DbSet<Il> Iller { get; set; }
        public DbSet<Ilce> Ilceler { get; set; }
        public DbSet<Mahalle> Mahalleler { get; set; }
        public DbSet<Tasinmaz> Tasinmaz { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Log> Log { get; set; }

    }
}
