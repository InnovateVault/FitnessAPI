using FitnessAPI.DTOs;
using FitnessAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitnessAPI.Repositories
{
    public interface IWorkoutRepository
    {
        List<Workout> GetAllWorkouts();

        Task AddWorkoutAsync(List<WorkoutsDTO> workoutDto);

        Workout FindWorkout(int id);
    }
}
