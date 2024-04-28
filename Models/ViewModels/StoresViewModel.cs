using CodeZoneStock.Models.DataEntities;
using System.ComponentModel.DataAnnotations;

namespace CodeZoneStock.Models.ViewModels
{
    public class StoresViewModel
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }
        public IEnumerable<Store>? Stores { get; set; }
    }
}
