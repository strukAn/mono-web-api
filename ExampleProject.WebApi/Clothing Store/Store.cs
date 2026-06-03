namespace ExampleProject.WebApi.Clothing_Store
{
    public class Store
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public List<Order> Orders { get; set; }
    }
}
