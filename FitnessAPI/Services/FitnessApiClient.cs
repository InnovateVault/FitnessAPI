using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FitnessAPI.Services;

namespace FitnessAPI.Client
{
    public class FitnessApiClient : IFitnessApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public FitnessApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _apiKey = Environment.GetEnvironmentVariable("FITNESS_API_KEY")
                ?? throw new InvalidOperationException("API Key is missing.");
        }

        public async Task<string> GetExerciseDataAsync(string endpoint, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(endpoint))
            {
                throw new ArgumentException("Endpoint cannot be null or whitespace.", nameof(endpoint));
            }

            var request = new HttpRequestMessage(HttpMethod.Get, new Uri(endpoint))
            {
                Headers =
                {
                    { "x-rapidapi-key", _apiKey },
                    { "x-rapidapi-host", "exercisedb.p.rapidapi.com" }
                }
            };

            using var response = await _httpClient.SendAsync(request, cancellationToken);
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync(cancellationToken);
            return body;
        }
    }
}
