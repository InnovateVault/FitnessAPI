using FitnessAPI.Data;
using FitnessAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FitnessAPI.Repositories
{
    /// <summary>
    /// Repository class for managing user-related operations.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IWorkoutRepository _workoutRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="context">Database context for accessing user data.</param>
        /// <param name="workoutRepository">Repository for workout-related operations.</param>
        public UserRepository(ApplicationDbContext context, IWorkoutRepository workoutRepository)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _workoutRepository = workoutRepository ?? throw new ArgumentNullException(nameof(workoutRepository));
        }

        /// <summary>
        /// Retrieves all users from the database.
        /// </summary>
        /// <returns>List of all users.</returns>
        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        /// <summary>
        /// Adds a workout to the user's list of favorite workouts.
        /// </summary>
        /// <param name="userId">ID of the user.</param>
        /// <param name="workoutId">ID of the workout to be added to favorites.</param>
        public void AddToUserFavorites(int userId, int workoutId)
        {
            // Find the user and workout from the respective repositories.
            var user = FindUser(userId);
            var workout = _workoutRepository.FindWorkout(workoutId);

            if (user == null || workout == null)
            {
                throw new KeyNotFoundException($"User with ID {userId} or Workout with ID {workoutId} not found.");
            }

            // Attempt to add the workout to the user's favorites.
            try
            {
                _context.Favorites.Add(new UserFavorites { UserId = user.Id, WorkoutId = workout.Id });
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while adding the workout to the user's favorites.", ex);
            }
        }

        /// <summary>
        /// Generates workout recommendations for a user based on their favorite muscle groups.
        /// </summary>
        /// <param name="userId">ID of the user.</param>
        public void AddToUserRecommendations(int userId)
        {
            // Find the user.
            var user = FindUser(userId);

            // Retrieve the muscle groups of the user's favorite workouts.
            var userFavoriteWorkouts = _context.Favorites
                .Where(x => x.UserId == userId)
                .Select(x => x.Workout!.MuscleGroup)
                .Distinct()
                .ToList();

            if (!userFavoriteWorkouts.Any())
            {
                throw new InvalidOperationException("No favorites found for the user.");
            }

            // Find workouts matching the favorite muscle groups.
            var recommendedWorkouts = _context.Workouts
                .Where(x => userFavoriteWorkouts.Contains(x.MuscleGroup))
                .ToList();

            if (!recommendedWorkouts.Any())
            {
                throw new InvalidOperationException("No workouts found to recommend based on the user's favorites.");
            }

            // Add the recommended workouts to the user's recommendations.
            try
            {
                foreach (var workout in recommendedWorkouts)
                {
                    _context.Recommendations.Add(new UserRecommendations
                    {
                        UserId = user.Id,
                        WorkoutId = workout.Id
                    });
                }
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while adding recommendations for the user.", ex);
            }
        }

        /// <summary>
        /// Adds a new user to the database.
        /// </summary>
        /// <param name="user">The user to add.</param>
        public void AddUser(User user)
        {
            ArgumentNullException.ThrowIfNull(user, nameof(user));

            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while saving the user to the database.", ex);
            }
        }

        /// <summary>
        /// Updates an existing user's details.
        /// </summary>
        /// <param name="id">ID of the user to update.</param>
        /// <param name="user">The updated user details.</param>
        public void UpdateUser(int id, User user)
        {
            // Find the user to update.
            var findUser = FindUser(id);

            try
            {
                // Update the user's properties with the new values.
                _context.Entry(findUser).CurrentValues.SetValues(user);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while updating the user.", ex);
            }
        }

        /// <summary>
        /// Deletes a user from the database.
        /// </summary>
        /// <param name="id">ID of the user to delete.</param>
        public void DeleteUser(int id)
        {
            // Find the user to delete.
            var user = FindUser(id);

            try
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while deleting the user from the database.", ex);
            }
        }

        /// <summary>
        /// Finds a user by their ID.
        /// </summary>
        /// <param name="id">ID of the user to find.</param>
        /// <returns>The user if found; otherwise, throws an exception.</returns>
        public User FindUser(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("User ID must be a positive integer.", nameof(id));
            }

            var findUser = _context.Users.Find(id)
                ?? throw new KeyNotFoundException($"User with ID {id} not found.");

            return findUser;
        }
    }
}
