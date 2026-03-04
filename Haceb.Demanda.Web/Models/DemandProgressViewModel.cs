using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Haceb.Demanda.Web.Models
{
    public class DemandProgressViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int TypeId { get; set; }
        [Required]
        public int Prioritize { get; set; }
        [Required]
        public int RatingId { get; set; }
        [Required]
        public int StateId { get; set; }
        [Required]
        public int UserId { get; set; }
        public string? Comments { get; set; }

        // Select lists
        public IEnumerable<SelectListItem>? Users { get; set; }
        public IEnumerable<SelectListItem>? Types { get; set; }
        public IEnumerable<SelectListItem>? Ratings { get; set; }
        public IEnumerable<SelectListItem>? Priorities { get; set; }
        public IEnumerable<SelectListItem>? Status { get; set; }

    }
}
