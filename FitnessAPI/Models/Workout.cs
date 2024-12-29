using System.ComponentModel.DataAnnotations;

namespace FitnessAPI.Models
{
    /// <summary>
    /// Represents the workouts in the Fitness API.
    /// </summary>
    public class Workout
    {
        /// <summary>
        /// Unique identifier for the workout.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Duration of the workout in minutes. Must be a positive number.
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "Duration must be a positive number greater than zero.")]
        public int? Duration { get; set; }

        /// <summary>
        /// Name of the workout (required).
        /// </summary>
        [Required(ErrorMessage = "Name is required.")]
        public required string Name { get; set; }

        /// <summary>
        /// Detailed description of the workout (optional, nullable).
        /// </summary>
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }

        /// <summary>
        /// Targeted muscle group for the workout (optional, nullable).
        /// </summary>
        [StringLength(100, ErrorMessage = "Muscle group cannot exceed 100 characters.")]
        public string? MuscleGroup { get; set; }

        /// <summary>
        /// Difficulty level of the workout (optional, nullable).
        /// </summary>
        [StringLength(50, ErrorMessage = "Difficulty cannot exceed 50 characters.")]
        public string? Difficulty { get; set; }

        /// <summary>
        /// Equipment needed for the workout (optional, nullable).
        /// </summary>
        [StringLength(100, ErrorMessage = "Equipment cannot exceed 100 characters.")]
        public string? Equipment { get; set; }
    }
}
