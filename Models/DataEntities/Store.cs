namespace CodeZoneStock.Models.DataEntities
{
    public class Store
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<Item>? Items { get; set; }
    }
}
