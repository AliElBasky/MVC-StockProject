using System.ComponentModel.DataAnnotations.Schema;

namespace CodeZoneStock.Models.DataEntities
{
    public class StoreItem
    {
        public int ItemId { get; set; }
        public int StoreId { get; set; }
        public int Quantity { get; set; }
        public Item Item { get; set; }
        public Store Store { get; set; }
    }
}
