using Baristasyon.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Baristasyon.Persistence.Contexts
{
    public class BaristasyonDbContext : DbContext
    {
        public BaristasyonDbContext(DbContextOptions<BaristasyonDbContext> options) : base(options)
        {
        }

        // DbSet'ler
        public DbSet<CoffeeRecipe> CoffeeRecipes => Set<CoffeeRecipe>();
        public DbSet<Equipment> Equipments => Set<Equipment>();
        public DbSet<JobPost> JobPosts => Set<JobPost>();
        public DbSet<User> Users => Set<User>();
        public DbSet<FavoriteRecipe> FavoriteRecipes => Set<FavoriteRecipe>();
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Rating> Ratings { get; set; }

        public DbSet<Blog> Blogs { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // FavoriteRecipe ilişkileri
            modelBuilder.Entity<FavoriteRecipe>()
                .HasKey(fr => fr.Id);

            modelBuilder.Entity<FavoriteRecipe>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(fr => fr.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FavoriteRecipe>()
                .HasOne<CoffeeRecipe>()
                .WithMany()
                .HasForeignKey(fr => fr.CoffeeRecipeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Review>()
            .HasOne(r => r.User)
            .WithMany()
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);


            // (Opsiyonel) String alanlara max length atayabilirsin
            modelBuilder.Entity<User>().Property(u => u.Username).HasMaxLength(50);
            modelBuilder.Entity<User>().Property(u => u.Email).HasMaxLength(100);
        }
    }
}
