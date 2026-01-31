using Microsoft.EntityFrameworkCore;

using MyApi.Data;
using MyApi.Repository;

namespace MyApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen();

            // Singleton  → one instance for the whole application
            // Scoped     → one instance per request
            // Transient  → one instance every time it's requested
            builder.Services.AddScoped<ITopicRepository, TopicRepository>();
            builder.Services.AddScoped<ICommentRepository, CommentRepository>();

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
