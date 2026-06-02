namespace ExampleProject.WebApi
{
    public static class Database
    {
        public static List<Category> Categories =
        [
            new Category(1, "Hrana i pice"),
            new Category(2, "Odjeca"),
            new Category(3, "Komponente")
        ];

        public static List<Product> Products =
        [
            new Product(id: 1, name: "Voda", description: "Mokra voda", stock: 312, Categories[0]),
            new Product(id: 2, name: "Hlace", description: "Dugacke hlace", stock: 13, Categories[1])
        ];
    }
}
