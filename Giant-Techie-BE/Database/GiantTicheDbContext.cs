using Giant_Techie_BE.Models;
using Microsoft.EntityFrameworkCore;

namespace Giant_Techie_BE.Database
{
    public class GiantTicheDbContext(DbContextOptions<GiantTicheDbContext> options) : DbContext(options)
    {
        public DbSet<Competitions> Competitions => Set<Competitions>();
        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("app");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GiantTicheDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = "Host=localhost;Database=GiantTechieDb;Username=postgres;Password=yourpassword";
                optionsBuilder.UseNpgsql(connectionString);
            }
            optionsBuilder.UseAsyncSeeding(async (context, _, CancellationToken) =>
            {
                var competitionData = await context.Set<Competitions>().FirstOrDefaultAsync(c => c.Title == "Sample Competition");
                if (competitionData == null)
                {
                    competitionData = Models.Competitions.Create("Sample Competition", "This is a sample competition description.", DateTimeOffset.UtcNow.AddDays(1), DateTimeOffset.UtcNow.AddDays(10), "Upcoming");
                    context.Set<Competitions>().Add(competitionData);
                    await context.SaveChangesAsync();
                }

            })
            .UseSeeding((context, _) =>
            {
                var competitionData =  context.Set<Competitions>().FirstOrDefault(c => c.Title == "Sample Competition");
                if (competitionData == null)
                {
                    competitionData = Models.Competitions.Create("Sample Competition", "This is a sample competition description.", DateTimeOffset.UtcNow.AddDays(1), DateTimeOffset.UtcNow.AddDays(10), "Upcoming");
                    context.Set<Competitions>().Add(competitionData);
                    context.SaveChanges();
                }
            });
        }
    }
}
