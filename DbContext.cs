using Microsoft.EntityFrameworkCore;
using APIAnnuaire.Models;

namespace APIAnnuaire
{
    public class APIDbContext : DbContext
    {
        public APIDbContext(DbContextOptions<APIDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Site> Sites { get; set; }
        public DbSet<Service> Services { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Configurez ici votre connexion SQLite
                optionsBuilder.UseSqlite("Data Source=data.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Vous pouvez ajouter ici des configurations spécifiques à vos modèles si nécessaire.
        }
    }
}
