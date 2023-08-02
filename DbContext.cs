namespace APIAnnuaire
{
    using APIAnnuaire.Models;
    using Microsoft.EntityFrameworkCore;

    public class APIDbContext : DbContext
    {
        public APIDbContext(DbContextOptions<APIDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Site> Sites { get; set; }
        public DbSet<Service> Services { get; set; }
    }

}
