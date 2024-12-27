namespace FitnessAPI.DTOs
{
    /// <summary>
    /// Represents data transfer object for workouts.
    /// </summary>
    public class WorkoutsDTO
    {
        /// <summary>
        /// Name of the workout.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Equipment required for the workout.
        /// </summary>
        public string? Equipment { get; set; }

        /// <summary>
        /// URL to a GIF demonstrating the workout.
        /// </summary>
        public string? GifUrl { get; set; }

        /// <summary>
        /// Body part targeted by the workout.
        /// </summary>
        public string? BodyPart { get; set; }

        /// <summary>
        /// Specific target muscle or area for the workout.
        /// </summary>
        public string? Target { get; set; }

        /// <summary>
        /// Instructions for performing the workout.
        /// </summary>
        public string? Instructions { get; set; }

        /// <summary>
        /// Difficulty level of the workout (e.g., Easy, Medium, Hard).
        /// </summary>
        public string? Difficulty { get; set; }

        /// <summary>
        /// Approximate duration of the workout in minutes.
        /// </summary>
        public int? Duration { get; set; }
    }
}
