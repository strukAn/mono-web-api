using ExampleProject.WebApi.Clothing_Store;

namespace ExampleProject.WebApi
{
    public class Order
    {
        public int Id { get; set; }
        public Employee Employee { get; set; }
        public Customer Customer { get; set; }
        public Store Store { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<ClothingItem> ClothingItems { get; set; }
    }
}
