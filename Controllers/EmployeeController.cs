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

        // Autres méthodes d'action pour les opérations POST, PUT, DELETE, etc.
    }
}
