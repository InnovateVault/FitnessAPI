using System.ComponentModel.DataAnnotations;

namespace FitnessAPI.Models
{
    /// <summary>
    /// Represents a user in the Fitness API.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the unique identifier for the user.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the user's name.
        /// </summary>
        /// <remarks>Name cannot be null or empty. Maximum length of 100 characters.</remarks>
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public required string Name { get; set; }

        /// <summary>
        /// Gets or sets the user's age.
        /// </summary>
        /// <remarks>Age must be a positive value.</remarks>
        [Range(1, 150, ErrorMessage = "Age must be between 1 and 150.")]
        public int Age { get; set; }

        /// <summary>
        /// Gets or sets the user's fitness level.
        /// </summary>
        /// <remarks>Examples: "Beginner", "Intermediate", "Advanced".</remarks>
        public string? FitnessLevel { get; set; }

        // Navigation property for user favorites
        public List<UserFavorites> Favorites { get; set; } = new List<UserFavorites>();

        // Navigation property for user recommendations
        public List<UserRecommendations> Recommendations { get; set; } = new List<UserRecommendations>();
    }
}

