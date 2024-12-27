namespace FitnessAPI.DTOs
{
    /// <summary>
    /// Represents a user in the Fitness API for client-facing purposes.
    /// </summary>
    public class UserDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier for the user.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the user's name.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user's age.
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Gets or sets the user's fitness level.
        /// </summary>
        public string? FitnessLevel { get; set; }

        /// <summary>
        /// Gets or sets the list of the user's favorite workouts.
        /// </summary>
        public List<FavoritesDTO> Favorites { get; set; } = new List<FavoritesDTO>();

        /// <summary>
        /// Gets or sets the list of recommendations for the user.
        /// </summary>
        public List<RecommendationsDTO> Recommendations { get; set; } = new List<RecommendationsDTO>();
    }
}
