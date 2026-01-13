using Microsoft.EntityFrameworkCore;

namespace EfConsoleApp.Console
{
    public class ApplicationDbContext : DbContext
    {
        private const string _connString = "Server=localhost\\SQLEXPRESS;Database=EfConsoleAppDb;Trusted_Connection=True;TrustServerCertificate=True;";
        public DbSet<Footballer> Footballers { get; set; }
        public DbSet<Agent> Agent { get; set; }
        public DbSet<Club> Club { get; set; }
        public DbSet<FootballerAgent> FootballersAgent { get; set; }
        public DbSet<TransferMarketData> TransferMarketData { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FootballerAgent>()
                .HasKey(fa => new { fa.FootballerId, fa.AgentId });
        }
    }
}