using FitnessAPI.Models;
using System.Collections.Generic;

namespace FitnessAPI.Repositories
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();
        User FindUser(int id);
        void AddToUserFavorites(int userId, int workoutId);
        void AddToUserRecommendations(int userId);
        void AddUser(User user);
        void UpdateUser(int id, User user);
        void DeleteUser(int id);
    }
}
