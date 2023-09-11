using APIAnnuaire.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace APIAnnuaire
{
    public class EmployeeContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("Server=localhost;Database=annuaire;User=root;Password=adm;Port=3306;",
                new MariaDbServerVersion(new Version(11, 1, 2)));
        }
    }

    public static class EmployeeData
    {
        public static List<Employee> LoadDataFromDatabase()
        {
            using var context = new EmployeeContext();

            return context.Employees.ToList();
        }
    }
}
