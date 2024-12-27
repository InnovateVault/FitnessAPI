using FitnessAPI.Models;

namespace FitnessAPI.Repositories
{
    public interface IWorkoutRepository
    {
        Workout FindWorkout(int id);
    }
}
