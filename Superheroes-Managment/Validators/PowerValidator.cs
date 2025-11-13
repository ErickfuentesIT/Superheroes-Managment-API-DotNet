using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Superheroes_Managment.Context;
using Superheroes_Managment.Models;

namespace Superheroes_Managment.Validators
{
    public class PowerValidator: AbstractValidator<Power>
    {
        private readonly AppDbContext _context;

        public PowerValidator(AppDbContext context)
        {
            _context = context;
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Nombre de poder obligatorio")
                .MaximumLength(50).WithMessage("El nombre del poder no debe ser mayor a 50 caracteres");

            RuleFor(p => p)
                .MustAsync(NoDuplicatePower).WithMessage("El heroe ya cuenta con este poder").OverridePropertyName("Message");

        }

        private async Task<bool> NoDuplicatePower(Power power, CancellationToken token)
        {

            bool exists = await _context.Powers.AnyAsync(p =>
                p.HeroId == power.HeroId &&
                p.Name.ToLower() == power.Name.ToLower() &&
                p.Id != power.Id,
                token);

            return !exists;
        }
    }
}
