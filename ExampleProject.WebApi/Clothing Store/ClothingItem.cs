namespace ExampleProject.WebApi
{
    public class ClothingItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Sizes { get; set; }
        public string Material { get; set; }
        public string Description { get; set; }
        public List<Order> Orders { get; set; }
    }
}
