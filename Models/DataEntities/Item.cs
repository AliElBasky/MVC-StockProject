namespace CodeZoneStock.Models.DataEntities
{
    public class Item
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<Store>? Stores { get; set; }
    }
}
