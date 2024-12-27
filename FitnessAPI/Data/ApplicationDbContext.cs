using FitnessAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FitnessAPI.Data
{
    /// <summary>
    /// Represents the database context for the Fitness API.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
        /// </summary>
        /// <param name="options">The options to be used by a <see cref="DbContext"/>.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        /// <summary>
        /// Gets or sets the Users in the context.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Gets or sets the Workouts in the context.
        /// </summary>
        public DbSet<Workout> Workouts { get; set; }

        /// <summary>
        /// Gets or sets the Favorites in the context.
        /// </summary>
        public DbSet<UserFavorites> Favorites { get; set; }

        /// <summary>
        /// Gets or sets the Recommendations in the context.
        /// </summary>
        public DbSet<UserRecommendations> Recommendations { get; set; }

        /// <summary>
        /// Configures the entity framework model to be used by this context.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for the context.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserFavorites>().HasKey(x => new { x.UserId, x.WorkoutId });
            modelBuilder.Entity<UserRecommendations>().HasKey(x => new { x.UserId, x.WorkoutId });
        }
    }
}
