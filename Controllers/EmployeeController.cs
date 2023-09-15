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
        [HttpPost("FirstName/{FirstName}/LastName/{LastName}/Department/{Department}/Email/{Email}/PhoneNumber/{PhoneNumber}/MobilePhone/{MobilePhone}/JobTitle/{JobTitle}/JobDescription/{JobDescription}/Site/{Site}/Service/{Service}")]
        public ActionResult InsertNewEmployee(
            string FirstName,
            string LastName,
            string Department,
            string Email,
            string PhoneNumber,
            string MobilePhone,
            string JobTitle,
            string JobDescription,
            string Site,
            string Service)
        {
            try
            {
                // Recherchez l'ID du Service dans la table Services
                int? serviceId = _context.Services
                    .Where(s => s.Service == Service)
                    .Select(s => (int?)s.ServiceId)
                    .FirstOrDefault();

                // Recherchez l'ID du Site dans la table Sites
                int? siteId = _context.Sites
                    .Where(s => s.City == Site)
                    .Select(s => (int?)s.SiteId)
                    .FirstOrDefault();

                // Créez un nouvel employé avec les IDs récupérés
                Employees employee = new Employees
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    Department = Department,
                    Email = Email,
                    PhoneNumber = PhoneNumber,
                    MobilePhone = MobilePhone,
                    JobTitle = JobTitle,
                    JobDescription = JobDescription,
                    ServiceId = serviceId,
                    SiteId = siteId
                };

                // Ajoutez le nouvel employé à la table Employees
                _context.Employees.Add(employee);

                // Enregistrez les modifications dans la base de données
                _context.SaveChanges();

                // Renvoyez une réponse HTTP appropriée
                return CreatedAtAction("InsertNewEmployee", new { id = employee.EmployeeId }, employee);
            }
            catch (Exception ex)
            {
                // Gérez les erreurs et renvoyez une réponse d'erreur appropriée si nécessaire
                return StatusCode(500, $"Une erreur s'est produite : {ex.Message}");
            }
        }
    }
}
