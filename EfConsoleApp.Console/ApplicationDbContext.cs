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
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FootballerAgent>()
                .HasKey(fa => new { fa.FootballerId, fa.AgentId });

            modelBuilder.Entity<Club>()
                .HasData(
                    new Club { Id = 1, Name = "Real Madrid", Country = "Spain" }
                );

            modelBuilder.Entity<Footballer>()
                .HasData(
                    new Footballer { Id = 1, FirstName = "Cristiano", LastName = "Ronaldo", JerseyNumber = 7, BirthDate = new DateTime(1985, 2, 5), ClubId = 1 }
                );

            modelBuilder.Entity<Agent>()
                .HasData(new Agent { Id = 1, Name = "Jorge Mendes" });

            modelBuilder.Entity<TransferMarketData>()
                .HasData(
                    new TransferMarketData
                    {
                        Id = 1,
                        Marketvalue = 1000000m,
                        ContractExpirationDate = new DateTime(2027, 1, 1),
                        FootballerId = 1,
                    });

            modelBuilder.Entity<FootballerAgent>()
                .HasData(
                    new FootballerAgent { FootballerId = 1, AgentId = 1 }
                );


        }
    }
}