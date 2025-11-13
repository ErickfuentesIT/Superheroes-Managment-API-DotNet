using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Superheroes_Managment.Context;
using Superheroes_Managment.Models;
using Superheroes_Managment.DTOs;
using System.Collections;

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
        public async Task<ActionResult<IEnumerable<HeroDto>>> GetHeroes() {

            var heroes = await _context.Heroes.Select(h => new HeroDto { Id = h.Id,
            Name = h.Name,
            Alias = h.Alias,
            PowersCount = h.Powers.Count()
            }).ToListAsync();
            return Ok(heroes);
        }

        [HttpGet("top-powers")]
        public async Task<ActionResult<IEnumerable<HeroDto>>> GetHeroesTopPowers() {


            var heroes = await _context.Heroes.Select(h => new HeroDto
            {
                Id = h.Id,
                Name = h.Name,
                Alias = h.Alias,
                PowersCount = h.Powers.Count()
            }).OrderByDescending(h => h.PowersCount).ToListAsync();
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
