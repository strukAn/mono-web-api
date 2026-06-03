namespace ExampleProject.WebApi.Clothing_Store
{
    public class OrderItem
    {
        public int Id { get; set; }
        public Order Order { get; set; }
        public ClothingItem ClothingItem { get; set; }
    }
}
