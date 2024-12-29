using FitnessAPI.DTOs;
using FitnessAPI.Repositories;
using FitnessAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FitnessAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkoutController : ControllerBase
    {
        public readonly IWorkoutRepository _workoutRepository;
        public readonly IFitnessApiClient _fitnessApiClient;

        public WorkoutController(IWorkoutRepository workoutRepository, IFitnessApiClient fitnessApiClient)
        {
            _workoutRepository = workoutRepository;
            _fitnessApiClient = fitnessApiClient;
        }




        [HttpGet("{endpoint}")]
        public async Task<IActionResult> GetWorkoutsFromExerciseDbAsync(string endpoint)
        {
            try
            {
                var fitnessResponse = await _fitnessApiClient.GetExerciseDataAsync(endpoint);

                if (fitnessResponse == null)
                {
                    return NotFound("No data found for the provided endpoint.");
                }

                var workoutDto = JsonConvert.DeserializeObject<List<WorkoutsDTO>>(fitnessResponse);

                if (workoutDto == null)
                {
                    return BadRequest("Failed to deserialize the response data.");
                }

                await _workoutRepository.AddWorkoutAsync(workoutDto);

                return Ok("Workout data successfully added.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An internal error occurred: {ex.Message}");
            }
        }



    }
}
