
namespace AdoNetDemo
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

        public void DisplayInfo()
        {
            Console.WriteLine($"Product ID: {Id}, Name: {Name}, Price: {Price:C}, Stock: {Stock}");
        }
    }
}
