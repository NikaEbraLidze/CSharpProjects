using System;
using System.Threading.Tasks;

using AdoNetDemo;

class Program
{
    static string connectionString =
            "Server=;Database=;Trusted_Connection=True;TrustServerCertificate=True;";

    static async Task Main()
    {
        ProductsRepository repository = new ProductsRepository(connectionString);
        var products = await repository.GetAllProductsAsync();

        foreach (Product product in products)
        {
            product.DisplayInfo();
        }

        Console.WriteLine("\nFetching product with ID 2:");
        var singleProduct = await repository.GetProductByIdAsync(2);
        if (singleProduct != null)
        {
            singleProduct.DisplayInfo();
        }
        else
        {
            Console.WriteLine("Product not found.");
        }

        Console.WriteLine("\nAdding a new product:");
        var newProduct = new Product
        {
            Name = "Headphones",
            Price = 25.99M,
            Stock = 150
        };
        int newProductId = await repository.AddProductAsync(newProduct);
        if (newProductId != -1)
        {
            Console.WriteLine($"New product added with ID: {newProductId}");
        }
        else
        {
            Console.WriteLine("Failed to add new product.");
        }

        Console.WriteLine("\nUpdating product with ID 2:");
        var updateProduct = new Product
        {
            Id = 2,
            Name = "Keyboard",
            Price = 70.0M,
            Stock = 10,
        };
        bool isUpdated = await repository.UpdateProductAsync(updateProduct);
        if (isUpdated)
        {
            Console.WriteLine($"Product updated successfully.");
        }
        else
        {
            Console.WriteLine("Failed to update product.");
        }

        Console.WriteLine("\nDeleteting product with ID 2:");
        bool isDeleted = await repository.DeleteProductByIdAsync(2);
        if (isDeleted)
        {
            Console.WriteLine($"Product deleted successfully.");
        }
        else
        {
            Console.WriteLine("Failed to delete product.");
        }
    }
}
