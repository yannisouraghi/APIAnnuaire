using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; 
using APIAnnuaire.Models;

namespace APIAnnuaire.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class SiteController : Controller
    {
        private readonly APIDbContext _context; // Utilisation du contexte de base de données

        public SiteController(APIDbContext context) // Injection du contexte de base de données via le constructeur
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Sites>> Get()
        {
            var sites = _context.Sites.ToList();

            return sites;
        }

    }
}
