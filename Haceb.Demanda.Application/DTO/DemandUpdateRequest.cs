using FluentValidation;
using Haceb.Demanda.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haceb.Demanda.Application.DTO
{
    public class DemandUpdateRequest
    {
        public int Id { get; set; }
        public int TypeId { get; set; }
        public DemandPrioritize Prioritize { get; set; }
        public int RatingId { get; set; }
        public DemandStatus Status { get; set; }
        public int UserId { get; set; }
        public string? Comments { get; set; }
    }

    public class DemandUpdateRequestValidator : AbstractValidator<DemandUpdateRequest>
    {
        public DemandUpdateRequestValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("El número de caso debe ser mayor que 0.");

            RuleFor(x => x.TypeId)
                .GreaterThan(0)
                .WithMessage("Debe ingresar un tipo demanda válido.");

            RuleFor(x => x.Prioritize)
                .IsInEnum()
                .WithMessage("Debe seleccionar una prioridad.");

            RuleFor(x => x.RatingId)
                .GreaterThan(0)
                .WithMessage("Debe ingresar una clasificación válida.");

            RuleFor(x => x.Status)
                .IsInEnum()
                .WithMessage("Debe seleccionar un estado.");

            RuleFor(x => x.UserId)
                .GreaterThan(0)
                .WithMessage("El usuario no es válido.");

            RuleFor(x => x.Comments)
                .MaximumLength(1000)
                .WithMessage("El comentario no puede superar los 1000 caracteres.");
        }
    }
}
