using System.ComponentModel.DataAnnotations;

namespace CodeZoneStock.Models.ViewModels
{
    public class StoreFormViewModel
    {
        [Required]
        public string? Name { get; set; }
    }
}
