using CodeZoneStock.Models.DataEntities;
using System.ComponentModel.DataAnnotations;

namespace CodeZoneStock.Models.ViewModels
{
    public class StockFormViewModel
    {
        [Required]
        public int ItemId { get; set; }
        
        [Required]
        public int StoreId { get; set; }
        
        [Required]
        public int Quantity { get; set; }
        public int TotalQuantity { get; set; }
        public ICollection<StoreFormViewModel>? Stores { get; set; }
        public ICollection<ItemFormViewModel>? Items { get; set; }
    }
}
