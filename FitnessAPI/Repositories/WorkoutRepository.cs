using FitnessAPI.Data;
using FitnessAPI.DTOs;
using FitnessAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessAPI.Repositories
{
    /// <summary>
    /// Repository class for managing workout data in the database.
    /// </summary>
    public class WorkoutRepository : IWorkoutRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkoutRepository"/> class.
        /// </summary>
        /// <param name="context">The database context for accessing workout data.</param>
        public WorkoutRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Retrieves all workouts from the database.
        /// </summary>
        /// <returns>A list of all workouts.</returns>
        public List<Workout> GetAllWorkouts()
        {
            return _context.Workouts.ToList();
        }

        /// <summary>
        /// Adds a list of workouts to the database asynchronously.
        /// </summary>
        /// <param name="workoutDto">A list of workout DTOs to be added.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task AddWorkoutAsync(List<WorkoutsDTO> workoutDto)
        {
            if (workoutDto == null || !workoutDto.Any())
            {
                throw new ArgumentNullException(nameof(workoutDto), "Workout data cannot be null or empty.");
            }

            var workoutEntities = workoutDto.Select(workout => new Workout
            {
                Name = workout.Name,
                MuscleGroup = workout.BodyPart,
                Description = workout.Instructions != null ? string.Join(" ", workout.Instructions) : string.Empty,
                Difficulty = workout.Difficulty,
                Duration = workout.Duration ?? 0,
                Equipment = workout.Equipment
            }).ToList();

            await _context.Workouts.AddRangeAsync(workoutEntities);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Finds a workout by its ID.
        /// </summary>
        /// <param name="id">The ID of the workout to find.</param>
        /// <returns>The workout object if found.</returns>
        public Workout FindWorkout(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Workout ID must be a positive integer.", nameof(id));
            }

            var workout = _context.Workouts.Find(id)
                ?? throw new KeyNotFoundException($"Workout with ID {id} was not found.");

            return workout;
        }

        /// <summary>
        /// Updates an existing workout in the database.
        /// </summary>
        /// <param name="id">The ID of the workout to update.</param>
        /// <param name="workoutDto">The updated workout details.</param>
        public void UpdateWorkout(int id, WorkoutsDTO workoutDto)
        {
            if (workoutDto == null)
            {
                throw new ArgumentNullException(nameof(workoutDto), "Workout data cannot be null.");
            }

            var existingWorkout = _context.Workouts.Find(id);
            if (existingWorkout == null)
            {
                throw new KeyNotFoundException($"Workout with ID {id} was not found.");
            }

            existingWorkout.Name = workoutDto.Name ?? existingWorkout.Name;
            existingWorkout.MuscleGroup = workoutDto.BodyPart ?? existingWorkout.MuscleGroup;
            existingWorkout.Description = workoutDto.Instructions != null
                ? string.Join(" ", workoutDto.Instructions)
                : existingWorkout.Description;
            existingWorkout.Difficulty = workoutDto.Difficulty ?? existingWorkout.Difficulty;
            existingWorkout.Duration = workoutDto.Duration ?? existingWorkout.Duration;
            existingWorkout.Equipment = workoutDto.Equipment ?? existingWorkout.Equipment;

            _context.Workouts.Update(existingWorkout);
            _context.SaveChanges();
        }
    }
}
