using FitnessAPI.DTOs;
using FitnessAPI.Models;
using FitnessAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FitnessAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        // Inject the IUserRepository into the constructor.
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        /// <summary>
        /// Retrieves all users from the database.
        /// </summary>
        /// <returns>A list of all users.</returns>
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            try
            {
                var users = _userRepository.GetAllUsers(); // Get all users
                var userDtos = users.Select(u => new UserDTO
                {
                    Id = u.Id,
                    Name = u.Name,
                    Age = u.Age,
                    FitnessLevel = u.FitnessLevel,
                    Favorites = u.Favorites.Select(f => new FavoritesDTO { Id = f.WorkoutId, WorkoutName = f.Workout!.Name }).ToList(),
                    Recommendations = u.Recommendations.Select(r => new RecommendationsDTO { Id = r.WorkoutId, WorkoutName = r.Workout!.Name, RecommendationSource = r.RecommendationSource! }).ToList()
                }).ToList(); // Directly map to DTO here

                return Ok(userDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the users: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a specific user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>The user details in the form of a UserDTO.</returns>
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            try
            {
                var user = _userRepository.FindUser(id); // Use repository method to find user
                var userDto = new UserDTO
                {
                    Id = user.Id,
                    Name = user.Name,
                    Age = user.Age,
                    FitnessLevel = user.FitnessLevel,
                    Favorites = user.Favorites.Select(f => new FavoritesDTO { Id = f.WorkoutId, WorkoutName = f.Workout!.Name }).ToList(),
                    Recommendations = user.Recommendations.Select(r => new RecommendationsDTO { Id = r.WorkoutId, WorkoutName = r.Workout!.Name, RecommendationSource = r.RecommendationSource! }).ToList()
                }; // Directly map to DTO here
                return Ok(userDto);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message); // Handle user not found
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the user: {ex.Message}");
            }
        }

        /// <summary>
        /// Adds a new user to the database.
        /// </summary>
        /// <param name="user">The user to add.</param>
        /// <returns>A response indicating the result of the operation.</returns>
        [HttpPost]
        public IActionResult AddUser([FromBody] User user)
        {
            try
            {
                _userRepository.AddUser(user); // Use repository to add user
                return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user); // Return Created response
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while adding the user: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates an existing user's details.
        /// </summary>
        /// <param name="id">The ID of the user to update.</param>
        /// <param name="user">The updated user details.</param>
        /// <returns>A response indicating the result of the operation.</returns>
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] User user)
        {
            try
            {
                _userRepository.UpdateUser(id, user); // Use repository method to update user
                return NoContent(); // Return NoContent (204) on successful update
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message); // Handle user not found
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the user: {ex.Message}");
            }
        }

        /// <summary>
        /// Deletes a user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>A response indicating the result of the operation.</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                _userRepository.DeleteUser(id); // Use repository method to delete user
                return NoContent(); // Return NoContent (204) on successful deletion
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message); // Handle user not found
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the user: {ex.Message}");
            }
        }

        /// <summary>
        /// Adds a workout to the user's list of favorites.
        /// </summary>
        /// <param name="userId">ID of the user.</param>
        /// <param name="workoutId">ID of the workout to add to favorites.</param>
        /// <returns>A response indicating the result of the operation.</returns>
        [HttpPost("{userId}/favorites/{workoutId}")]
        public IActionResult AddToUserFavorites(int userId, int workoutId)
        {
            try
            {
                _userRepository.AddToUserFavorites(userId, workoutId); // Add workout to user favorites
                return Ok("Workout added to favorites.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while adding the workout to favorites: {ex.Message}");
            }
        }

        /// <summary>
        /// Generates workout recommendations for the user based on their favorite muscle groups.
        /// </summary>
        /// <param name="userId">ID of the user for whom to generate recommendations.</param>
        /// <returns>A response indicating the result of the operation.</returns>
        [HttpPost("{userId}/recommendations")]
        public IActionResult AddToUserRecommendations(int userId)
        {
            try
            {
                _userRepository.AddToUserRecommendations(userId); // Generate recommendations for the user
                return Ok("Recommendations generated.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while generating recommendations: {ex.Message}");
            }
        }
    }
}
