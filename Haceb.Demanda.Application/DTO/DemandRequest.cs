using FluentValidation;
using Haceb.Demanda.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haceb.Demanda.Application.DTO
{
    public class DemandRequest
    {
        public string PlaintiffName { get; set; }
        public string Description { get; set; }
        public int TypeId { get; set; }
        public int RatingId { get; set; }
        public int? UserId { get; set; }
    }

    public class DemandRequestValidator : AbstractValidator<DemandRequest>
    {
        public DemandRequestValidator()
        {
            RuleFor(x => x.PlaintiffName)
                .NotEmpty()
                .WithMessage("El nombre es obligatorio.")
                .MaximumLength(100)
                .WithMessage("El nombre no puede superar los 100 caracteres.");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("La descripción es obligatorio.")
                .MaximumLength(1000)
                .WithMessage("La Descripción no puede superar los 1000 caracteres.");

            RuleFor(x => x.TypeId)
                .GreaterThan(0)
                .WithMessage("Debe ingresar un tipo demanda válido.");

            RuleFor(x => x.RatingId)
                .GreaterThan(0)
                .WithMessage("Debe ingresar una clasificación válida.");
        }
    }
}
