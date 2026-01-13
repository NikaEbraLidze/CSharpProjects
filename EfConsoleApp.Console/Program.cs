using EfConsoleApp.Console;

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

class Program
{
    static async Task Main()
    {
        // try
        // {
        //     using (var context = new ApplicationDbContext())
        //     {

        //         var footballers = context.Footballers.ToList();
        //         foreach (var footballer in footballers)
        //         {
        //             Console.WriteLine($"Footballer: {footballer.FirstName} {footballer.LastName}, Jersey Number: {footballer.JerseyNumber}");
        //         }
        //     }
        // }
        // catch (SqlException ex)
        // {
        //    Console.WriteLine($"SQL error: {ex.Message}");
        // }
        // catch (Exception ex)
        // {
        //     Console.WriteLine($"Fatal error: {ex.Message}");
        // }

        // try
        // {
        //     using (var context = new ApplicationDbContext())
        //     {

        //         var newFootballer = new Footballer
        //         {
        //             FirstName = "Lionel",
        //             LastName = "Messi",
        //             JerseyNumber = 10,
        //             BirthDate = new DateTime(1987, 6, 24),
        //             ClubId = 1,
        //         };
        //         context.Footballers.Add(newFootballer);
        //         await context.SaveChangesAsync();
        //         Console.WriteLine("New footballer added successfully.");
        //     }
        // }
        // catch (SqlException ex)
        // {
        //     Console.WriteLine($"SQL error: {ex.Message}");
        // }
        // catch (Exception ex)
        // {
        //     Console.WriteLine($"Fatal error: {ex.Message}");
        // }

        // try
        // {
        //     await using (var context = new ApplicationDbContext())
        //     {
        //         Footballer footballer = await context.Footballers.FirstOrDefaultAsync(f => f.Id == 4);
        //         footballer.ClubId = 2;

        //         await context.SaveChangesAsync();
        //         Console.WriteLine("Footballer updated successfully.");
        //     }
        // }
        // catch (SqlException ex)
        // {
        //     Console.WriteLine($"SQL error: {ex.Message}");
        // }
        // catch (Exception ex)
        // {
        //     Console.WriteLine($"Fatal error: {ex.Message}");
        // }


        try
        {
            await using (var context = new ApplicationDbContext())
            {
                Footballer footballer = await context.Footballers.FirstOrDefaultAsync(f => f.Id == 4);
                if (footballer != null) context.Footballers.Remove(footballer);

                await context.SaveChangesAsync();
                Console.WriteLine("Footballer deleted successfully.");
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"SQL error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fatal error: {ex.Message}");
        }
    }
}