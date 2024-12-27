namespace RPG.Data
{
    using Microsoft.EntityFrameworkCore;
    using RPG.Data.Entities;
    using RPG.Data.Entities.GameEntityTypes;

    public class RPGDbContext : DbContext
    {

        public RPGDbContext(DbContextOptions<RPGDbContext> options) 
            : base(options) { }

        public DbSet<Character> Characters { get; set; } = null!;
        public DbSet<GameSession> GameSessions { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
            .Entity<GameSession>()
            .HasOne(c => c.Character)
            .WithOne(c => c.GameSession);

            base.OnModelCreating(modelBuilder);
        }

    }
}
