namespace Haceb.Demanda.Web.Models
{
    public class DemandDetailViewModel : DemandIndexViewModel
    {
        public List<DemandProgressDetailViewModel> ProgressHistory { get; set; } = new();
    }

    public class DemandProgressDetailViewModel
    {
        public int Id { get; set; }
        public string Comments { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UserName { get; set; }
    }
}
