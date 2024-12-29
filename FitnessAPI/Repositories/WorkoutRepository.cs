using FitnessAPI.Data;
using FitnessAPI.DTOs;
using FitnessAPI.Models;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace FitnessAPI.Repositories
{
    /// <summary>
    /// Provides methods to manage and access workout data.
    /// </summary>
    public class WorkoutRepository : IWorkoutRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkoutRepository"/> class.
        /// </summary>
        /// <param name="context">Database context for accessing workout data.</param>
        public WorkoutRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Retrieves all workouts from the local database.
        /// </summary>
        /// <returns>A list of all workouts.</returns>
        public List<Workout> GetAllWorkouts()
        {
            return _context.Workouts.ToList();
        }

        /// <summary>
        /// Adds a new workout, communicating with an external API.
        /// </summary>
        /// <param name="workoutDto">The workout details to add.</param>
        public async Task AddWorkoutAsync(List<WorkoutsDTO> workoutDto)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(workoutDto);

                if (!workoutDto.Any())
                {
                    throw new InvalidOperationException("The workout list is empty.");
                }

                foreach (var workout in workoutDto)
                {
                    var workoutEntity = new Workout
                    {
                        Name = workout.Name,
                        MuscleGroup = workout.BodyPart,
                        Description = workout.Instructions != null ? string.Join(" ", workout.Instructions) : string.Empty,
                        Difficulty = workout.Difficulty,
                        Duration = workout.Duration ?? 0,
                        Equipment = workout.Equipment
                    };

                    await _context.Workouts.AddAsync(workoutEntity);
                }

                await _context.SaveChangesAsync();
            }
            catch (ArgumentNullException ex)
            {
                throw new InvalidOperationException("The workout list cannot be null.", ex);
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException("The workout list is empty.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the workouts.", ex);
            }
        }

        /// <summary>
        /// Finds a workout by its ID in the local database.
        /// </summary>
        /// <param name="id">The ID of the workout to find.</param>
        /// <returns>The workout if found; otherwise, throws an exception.</returns>
        public Workout FindWorkout(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Workout ID must be a positive integer.", nameof(id));

            var workout = _context.Workouts.Find(id)
                ?? throw new KeyNotFoundException($"Workout with ID {id} was not found.");

            return workout;
        }
    }
}
