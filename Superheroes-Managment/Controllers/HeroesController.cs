using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Superheroes_Managment.Context;
using Superheroes_Managment.Models;

namespace Superheroes_Managment.Controllers
{
    [ApiController]
    [Route("heroes")]
    public class HeroesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public HeroesController(AppDbContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hero>>> GetHeroes() {

            var heroes = await _context.Heroes.ToListAsync();
            return Ok(heroes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Hero>> GetHero(int id) {
            var hero = await _context.Heroes.Include(h => h.Powers).FirstOrDefaultAsync(h => h.Id == id
            );
            if (hero == null) {
                return NotFound();
                    }
            return Ok(hero);
        }
    }
}
