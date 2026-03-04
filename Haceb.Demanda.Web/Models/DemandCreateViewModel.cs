using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Haceb.Demanda.Web.Models
{
    public class DemandCreateViewModel
    {
        public int Id { get; set; }
        [Required]
        public string PlaintiffName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int TypeId { get; set; }
        [Required]
        public int RatingId { get; set; }
        [Required]
        public int UserId { get; set; }

        // Select lists
        public IEnumerable<SelectListItem>? Users { get; set; }
        public IEnumerable<SelectListItem>? Types { get; set; }
        public IEnumerable<SelectListItem>? Ratings { get; set; }
    }
}
