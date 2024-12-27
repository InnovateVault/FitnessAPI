using FitnessAPI.Data;
using FitnessAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FitnessAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IWorkoutRepository _workoutRepository;

        public UserRepository(ApplicationDbContext context, IWorkoutRepository workoutRepository)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _workoutRepository = workoutRepository ?? throw new ArgumentNullException(nameof(workoutRepository));
        }

        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public void AddToUserFavorites(int userId, int workoutId)
        {
            var user = FindUser(userId);
            var workout = _workoutRepository.FindWorkout(workoutId);

            if (user == null || workout == null)
            {
                throw new KeyNotFoundException($"User with ID {userId} or Workout with ID {workoutId} not found.");
            }

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

        public void AddToUserRecommendations(int userId)
        {
            var user = FindUser(userId);

            var userFavoriteWorkouts = _context.Favorites
                .Where(x => x.UserId == userId)
                .Select(x => x.Workout!.MuscleGroup)
                .Distinct()
                .ToList();

            if (!userFavoriteWorkouts.Any())
            {
                throw new InvalidOperationException("No favorites found for the user.");
            }

            var recommendedWorkouts = _context.Workouts
                .Where(x => userFavoriteWorkouts.Contains(x.MuscleGroup))
                .ToList();

            if (!recommendedWorkouts.Any())
            {
                throw new InvalidOperationException("No workouts found to recommend based on the user's favorites.");
            }

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

        public void UpdateUser(int id, User user)
        {
            var findUser = FindUser(id);

            try
            {
                _context.Entry(findUser).CurrentValues.SetValues(user);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while updating the user.", ex);
            }
        }

        public void DeleteUser(int id)
        {
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
