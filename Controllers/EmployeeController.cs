using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Ajout de cette référence
using APIAnnuaire.Models;

namespace APIAnnuaire.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeContext _context; // Utilisation du contexte de base de données

        public EmployeeController(EmployeeContext context) // Injection du contexte de base de données via le constructeur
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Employee>> Get()
        {
            var employees = _context.Employees.ToList(); // Charger les employés depuis la base de données

            return employees;
        }

        [HttpGet("{id}")]
        public ActionResult<Employee> Get(int id)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        [HttpGet("site/{Site}")]
        public ActionResult<List<Employee>> GetEmployeesBySite(string Site)
        {
            var employeesBySite = _context.Employees
                .Where(e => e.Site.Equals(Site, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (employeesBySite.Count == 0)
            {
                return NotFound();
            }

            return employeesBySite;
        }

        [HttpGet("service/{Service}")]
        public ActionResult<List<Employee>> GetEmployeesByService(string Service)
        {
            var employeesByService = _context.Employees
                .Where(e => e.Service.Equals(Service, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (employeesByService.Count == 0)
            {
                return NotFound();
            }

            return employeesByService;
        }

        [HttpGet("lastname/{LastName}")]
        public ActionResult<List<Employee>> GetEmployeesByLastName(string LastName)
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
    }
}
