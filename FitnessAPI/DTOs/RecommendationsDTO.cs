using FitnessAPI.Helpers;

namespace FitnessAPI.DTOs
{
    /// <summary>
    /// Data Transfer Object representing a recommended workout in the Fitness API.
    /// </summary>
    public class RecommendationsDTO : BaseDTO
    {
        /// <summary>
        /// Gets or sets the source of the recommendation.
        /// </summary>
        public string RecommendationSource { get; set; } = "AI Recommendation or User Interaction";
    }
}