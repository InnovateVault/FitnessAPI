using FitnessAPI.Models;

namespace FitnessAPI.Helpers
{
    /// <summary>
    /// Represents a logical relationship between a user and a workout.
    /// This class is not part of the database schema but serves as a helper for managing user-workout interactions.
    /// </summary>
    public class UserWorkoutRelation
    {
        /// <summary>
        /// Gets or sets the unique identifier of the user involved in the relationship.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the workout involved in the relationship.
        /// </summary>
        public int WorkoutId { get; set; }

        /// <summary>
        /// Gets or sets the user entity associated with this relationship.
        /// </summary>
        public User? User { get; set; }

        /// <summary>
        /// Gets or sets the workout entity associated with this relationship.
        /// </summary>
        public Workout? Workout { get; set; }
    }
}
