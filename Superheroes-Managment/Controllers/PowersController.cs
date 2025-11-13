using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using Superheroes_Managment.Context;
using Superheroes_Managment.Models;
using System.ComponentModel.DataAnnotations;

namespace Superheroes_Managment.Controllers
{
    [ApiController]
    [Route("powers")]
    public class PowersController : ControllerBase {

        private readonly AppDbContext _context;

        private readonly IValidator<Power> _validator;
        public PowersController(AppDbContext context, IValidator<Power> validator)
        {
            _context = context;
            _validator = validator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Power>>> getPowers([FromQuery] int? heroId) {

            var query = _context.Powers.AsQueryable();

            if (heroId.HasValue) {

                query = query.Where(p => p.HeroId == heroId.Value);
            }

            var powers = await query.ToListAsync();
            return Ok(powers);
        }

        [HttpPost]
        public async Task<ActionResult<Power>> CreatePower(Power power)
        {

            var validationResult = await _validator.ValidateAsync(power);

            if (!validationResult.IsValid) {

                return BadRequest(validationResult.ToDictionary());

            }

            var heroExists = await _context.Heroes.AnyAsync(h => h.Id == power.HeroId);

            if (!heroExists) {

                return BadRequest($"No existe un héroe con el Id {power.HeroId}");
            
            }

            _context.Powers.Add(power);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(getPowers), 
                new { id = power.Id },
                power);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePower(int id, Power power) {

            if (id != power.Id) {

                return BadRequest("No coincide el ID del cuerpo con la URL");

            }

            var heroExists = await _context.Heroes.AnyAsync(h => h.Id == power.HeroId);

            if (!heroExists)
            {

                return BadRequest($"No existe un héroe con el Id {power.HeroId}");

            }

            _context.Entry(power).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PowerExists(id))
                {
                    return NotFound();
                }
            }

            return NoContent();



        }

        private bool PowerExists(int id)
        {
            return _context.Powers.Any(e => e.Id == id);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePower(int id) {

            var power = await _context.Powers.FindAsync(id);

            if (power == null)
            {
                return NotFound();
            }

            _context.Powers.Remove(power);

            await _context.SaveChangesAsync();

            return NoContent();


        }


    }


}

