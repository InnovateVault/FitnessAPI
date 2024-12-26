using FitnessAPI.Helpers;

namespace FitnessAPI.Models
{
    /// <summary>
    /// Represents a user's favorite workout.
    /// Inherits properties from <see cref="UserWorkoutRelation"/> to define the relationship between the user and the workout.
    /// </summary>
    public class UserFavorites : UserWorkoutRelation
    {
        /// <summary>
        /// Gets or sets the date when the workout was marked as a favorite.
        /// </summary>
        public DateTime FavoriteDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this workout is marked as a favorite.
        /// </summary>
        public bool IsFavorite { get; set; }
    }
}
