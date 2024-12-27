namespace FitnessAPI.Helpers
{
    /// <summary>
    /// Base class for common properties of DTOs in the Fitness API.
    /// </summary>
    public class BaseDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the workout name.
        /// </summary>
        public string WorkoutName { get; set; } = string.Empty;
    }
}
