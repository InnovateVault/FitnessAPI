using FitnessAPI.Helpers;

namespace FitnessAPI.Models
{
    /// <summary>
    /// Represents a recommendation for a user to engage in a specific workout.
    /// Inherits properties from <see cref="UserWorkoutRelation"/> to define the relationship between the user and the workout.
    /// </summary>
    public class UserRecommendations : UserWorkoutRelation
    {
        /// <summary>
        /// Gets or sets the source of the recommendation, such as "AI Engine" or "Trainer Suggestion".
        /// </summary>
        public string? RecommendationSource { get; set; }

        /// <summary>
        /// Gets or sets the date when the recommendation was made.
        /// </summary>
        public DateTime RecommendedDate { get; set; }
    }
}
