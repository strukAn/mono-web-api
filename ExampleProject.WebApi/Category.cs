using System.Data.SqlTypes;

namespace ExampleProject.WebApi
{
    public class Category
    {
        public int Id { get; }
        public string Name { get; set; }
        public Category(string name = "")
        {
            this.Id = Database.Categories.Last().Id + 1;
            this.Name = name;
        }
        public Category(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }
}
