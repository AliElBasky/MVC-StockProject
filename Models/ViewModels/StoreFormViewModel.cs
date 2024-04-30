using System.ComponentModel.DataAnnotations;

namespace CodeZoneStock.Models.ViewModels
{
    public class StoreFormViewModel
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
    }
}
