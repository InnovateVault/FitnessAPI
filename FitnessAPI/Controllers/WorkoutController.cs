using FitnessAPI.DTOs;
using FitnessAPI.Repositories;
using FitnessAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FitnessAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkoutsController : ControllerBase
    {
        private readonly IWorkoutRepository _workoutRepository;
        private readonly IFitnessApiClient _fitnessApiClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkoutController"/> class.
        /// </summary>
        /// <param name="workoutRepository">Repository to manage workout data.</param>
        /// <param name="fitnessApiClient">Service to interact with external exercise data API.</param>
        public WorkoutsController(IWorkoutRepository workoutRepository, IFitnessApiClient fitnessApiClient)
        {
            _workoutRepository = workoutRepository;
            _fitnessApiClient = fitnessApiClient;
        }

        /// <summary>
        /// Retrieves workout data from an external API and stores it in the database.
        /// </summary>
        /// <param name="endpoint">The API endpoint to fetch data from.</param>
        /// <returns>A success message or an error response.</returns>
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

        /// <summary>
        /// Retrieves all workouts from the database.
        /// </summary>
        /// <returns>A list of workouts.</returns>
        [HttpGet]
        public IActionResult GetAllWorkouts()
        {
            try
            {
                var workouts = _workoutRepository.GetAllWorkouts();
                return Ok(workouts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An internal error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates a workout in the database.
        /// </summary>
        /// <param name="id">The ID of the workout to update.</param>
        /// <param name="workoutDto">The updated workout details.</param>
        /// <returns>A success message or an error response.</returns>
        [HttpPut("{id}")]
        public IActionResult UpdateWorkout(int id, WorkoutsDTO workoutDto)
        {
            try
            {
                _workoutRepository.UpdateWorkout(id, workoutDto);
                return Ok("Workout updated successfully.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An internal error occurred: {ex.Message}");
            }
        }
    }
}
