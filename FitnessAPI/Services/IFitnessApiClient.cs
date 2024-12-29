namespace FitnessAPI.Services
{
    public interface IFitnessApiClient
    {
        Task<string> GetExerciseDataAsync(string endpoint, CancellationToken cancellationToken = default);
    }
}
