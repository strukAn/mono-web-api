namespace ExampleProject.WebApi.Clothing_Store
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Store Store { get; set; }
        public DateOnly StartedOn { get; set; }
        public List<Order> Orders { get; set; }
    }
}
