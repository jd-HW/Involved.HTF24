using Involved.HTF.Common.Dto;
using System.Net.Http.Json;

namespace Involved.HTF.Common.Services
{
    public class AqualonService
    {
        private readonly HackTheFutureClient _client;

        public AqualonService(HackTheFutureClient client)
        {
            _client = client;
        }

        public async Task<CommandDto> GetSampleChallengeOne()
        {
            var response = await _client.GetAsync($"/api/a/easy/puzzle");

            if (!response.IsSuccessStatusCode)
                throw new Exception("You weren't able to log in, did you provide the correct credentials?");

            return await response.Content.ReadFromJsonAsync<CommandDto>();
        }

        public async Task<string> PostSampleChallengeOne(int result)
        {
            var postRequest = await _client.PostAsJsonAsync($"/api/a/easy/puzzle", result);

            if (!postRequest.IsSuccessStatusCode)
                throw new Exception("You weren't able to log in, did you provide the correct credentials?");

            return await postRequest.Content.ReadAsStringAsync();
        }

        public int CalculatePosition(string input)
        {
            string[] instructions = input.Split(',');

            int depthPerMeter = 0;
            int totalDistance = 0;
            int totalDepth = 0;

            foreach (string instruction in instructions)
            {
                string[] parts = instruction.Trim().Split(' '); // Split "Down 7" into ["Down", "7"]
                string command = parts[0];
                int value = int.Parse(parts[1]);

                if (command == "Down")
                {
                    depthPerMeter += value; // Increase depth per meter
                }
                else if (command == "Up")
                {
                    depthPerMeter -= value; // Decrease depth per meter
                }
                else if (command == "Forward")
                {
                    totalDistance += value;             // Add to the distance
                    totalDepth += depthPerMeter * value; // Accumulate depth based on depthPerMeter
                }
            }

            // Calculate the final result: Depth * Distance
            return totalDepth * totalDistance;
        }
    }
}
