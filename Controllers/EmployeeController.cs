using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using APIAnnuaire.Models;

namespace APIAnnuaire.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly List<Employee> _employees;

        public EmployeeController()
        {
            string xmlFilePath = "EmployeeData.xml";
            _employees = EmployeeData.LoadDataFromXml(xmlFilePath); // Mettez à jour la liste _employees avec les données chargées depuis le fichier XML
        }

        [HttpGet]
        public ActionResult<IEnumerable<Employee>> Get()
        {
            return _employees.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Employee> Get(int id)
        {
            var employee = _employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        [HttpGet("site/{Site}")]
        public ActionResult<List<Employee>> GetEmployeesBySite(string Site)
        {
            var employeesBySite = _employees.Where(e => e.Site.Equals(Site, StringComparison.OrdinalIgnoreCase)).ToList();

            if (employeesBySite.Count == 0)
            {
                return NotFound();
            }

            return employeesBySite;
        }

        [HttpGet("service/{Service}")]
        public ActionResult<List<Employee>> GetEmployeesByService(string Service)
        {
            var employeesByService = _employees.Where(e => e.Service.Equals(Service, StringComparison.OrdinalIgnoreCase)).ToList();

            if (employeesByService.Count == 0)
            {
                return NotFound();
            }

            return employeesByService;
        }

        [HttpGet("lastname/{LastName}")]
        public ActionResult<List<Employee>> GetEmployeesByLastName(string LastName)
        {
            var employeesByLastName = _employees.Where(e => e.LastName.Equals(LastName, StringComparison.OrdinalIgnoreCase)).ToList();

            if (employeesByLastName.Count == 0)
            {
                return NotFound();
            }

            return employeesByLastName;
        }
    }
}
