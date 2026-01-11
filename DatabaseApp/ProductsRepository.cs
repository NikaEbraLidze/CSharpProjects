using System.Data.SqlTypes;

using Microsoft.Data.SqlClient;

namespace AdoNetDemo
{
    public class ProductsRepository
    {
        private readonly string _connString;
        public ProductsRepository(string connString)
        {
            _connString = connString;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            var products = new List<Product>();
            string sql = "SELECT * FROM Products";

            try
            {
                using SqlConnection connection = new SqlConnection(_connString);
                using SqlCommand command = new SqlCommand(sql, connection);

                await connection.OpenAsync();
                using SqlDataReader result = await command.ExecuteReaderAsync();

                while (await result.ReadAsync())
                {
                    products.Add(new Product
                    {
                        Id = result.GetInt32(result.GetOrdinal("Id")),
                        Name = result.GetString(result.GetOrdinal("Name")),
                        Price = result.GetDecimal(result.GetOrdinal("Price")),
                        Stock = result.GetInt32(result.GetOrdinal("Stock"))
                    });
                }
                return products;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("❌ SQL Error:");
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Error:");
                Console.WriteLine(ex.Message);
            }

            return products;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            Product product = null;
            string sql = @"SELECT Id, Name, Price, Stock FROM Products WHERE Id = @Id";

            try
            {
                await using SqlConnection connection = new SqlConnection(_connString);
                await using SqlCommand command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@Id", id);
                await connection.OpenAsync();
                await using SqlDataReader result = await command.ExecuteReaderAsync();

                while (await result.ReadAsync())
                {
                    product = new Product
                    {
                        Id = result.GetInt32(result.GetOrdinal("Id")),
                        Name = result.GetString(result.GetOrdinal("Name")),
                        Price = result.GetDecimal(result.GetOrdinal("Price")),
                        Stock = result.GetInt32(result.GetOrdinal("Stock"))
                    };
                }

                return product;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("❌ SQL Error:");
                Console.WriteLine(ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Error:");
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<int> AddProductAsync(Product product)
        {
            string sql = @"INSERT INTO Products (Name, Price, Stock) 
            VALUES (@Name, @Price, @Stock) 
            SELECT CAST(SCOPE_IDENTITY() AS INT);";

            try
            {
                await using SqlConnection connection = new SqlConnection(_connString);
                await using SqlCommand command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@Name", product.Name);
                command.Parameters.AddWithValue("@Price", product.Price);
                command.Parameters.AddWithValue("@Stock", product.Stock);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();

                int newId = (int)await command.ExecuteScalarAsync();
                product.Id = newId;

                Console.WriteLine($"✅ Product added successfully. New Product ID: {newId}");
                return newId;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("❌ SQL Error:");
                Console.WriteLine(ex.Message);
                return -1;
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Error:");
                Console.WriteLine(ex.Message);
                return -1;
            }
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            string sql = @"UPDATE Products SET Name = @Name, Price = @Price, Stock = @Stock WHERE Id = @Id";

            try
            {
                await using SqlConnection connection = new SqlConnection(_connString);
                await using SqlCommand command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@Id", product.Id);
                command.Parameters.AddWithValue("@Name", product.Name);
                command.Parameters.AddWithValue("@Price", product.Price);
                command.Parameters.AddWithValue("@Stock", product.Stock);

                await connection.OpenAsync();
                int rowsAffected = await command.ExecuteNonQueryAsync();

                if (rowsAffected > 0)
                {
                    Console.WriteLine("✅ Product updated successfully.");
                    return true;
                }
                else
                {
                    Console.WriteLine("⚠️ No product found with the given ID.");
                    return false;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("❌ SQL Error:");
                Console.WriteLine(ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Error:");
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteProductByIdAsync(int id)
        {
            string sql = @"DELETE FROM Products WHERE Id = @Id";

            try
            {
                await using SqlConnection connection = new SqlConnection(_connString);
                await using SqlCommand command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@Id", id);

                await connection.OpenAsync();

                int rowsAffected = await command.ExecuteNonQueryAsync();

                if (rowsAffected > 0)
                {
                    Console.WriteLine("✅ Product deleted successfully.");
                    return true;
                }
                else
                {
                    Console.WriteLine("⚠️ No product found with the given ID.");
                    return false;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("❌ SQL Error:");
                Console.WriteLine(ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Error:");
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}