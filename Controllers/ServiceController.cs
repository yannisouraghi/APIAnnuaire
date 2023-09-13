using APIAnnuaire.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIAnnuaire.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceController : ControllerBase
    {
        private readonly APIDbContext _context;

        public ServiceController(APIDbContext context)
        {
            _context = context;
        }

        // Ajoutez cette action pour obtenir la liste des services
        [HttpGet("Services")]
        public async Task<ActionResult<IEnumerable<Services>>> GetServices()
        {
            try
            {
                var services = await _context.Services.ToListAsync();

                if (services == null || services.Count == 0)
                {
                    return NotFound("Aucun service trouvé.");
                }

                return Ok(services);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Une erreur interne du serveur s'est produite : {ex.Message}");
            }
        }

        // ...
    }
}
