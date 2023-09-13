using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIAnnuaire.Models;

namespace APIAnnuaire.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly APIDbContext _context; // Utilisation du contexte de base de données

        public EmployeeController(APIDbContext context) // Injection du contexte de base de données via le constructeur
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Employees>> Get()
        {
            var employees = _context.Employees.ToList(); // Charger les employés depuis la base de données

            return employees;
        }

        [HttpGet("lastname/{LastName}")]
        public ActionResult<List<Employees>> GetEmployeesByLastName(string LastName)
        {
            var employeesByLastName = _context.Employees
                .Where(e => e.LastName.Equals(LastName, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (employeesByLastName.Count == 0)
            {
                return NotFound();
            }

            return employeesByLastName;
        }

        [HttpGet("Search")]
        public ActionResult<IEnumerable<object>> SearchEmployees(string name = null, string site = null, string service = null)
        {
            var query = _context.Employees.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e => EF.Functions.Like(e.LastName, $"%{name}%"));
            }

            if (!string.IsNullOrEmpty(site))
            {
                query = query.Where(e => EF.Functions.Like(e.Sites.City, $"%{site}%"));
            }

            if (!string.IsNullOrEmpty(service))
            {
                query = query.Where(e => EF.Functions.Like(e.Services.Service, $"%{service}%"));
            }

            // Maintenant, vous pouvez exécuter la requête en utilisant ToList() ou une autre méthode de résultat.
            var employees = query.Select(e => new
            {
                e.EmployeeId,
                e.FirstName,
                e.LastName,
                e.Department,
                e.Email,
                e.PhoneNumber,
                e.MobilePhone,
                e.JobTitle,
                e.JobDescription,
                Services = e.Services != null ? e.Services.Service : null, // Remplacer ServiceId par Services
                City = e.Sites != null ? e.Sites.City : null // Remplacer SiteId par City
            }).ToList();

            return employees;
        }

    }
}
