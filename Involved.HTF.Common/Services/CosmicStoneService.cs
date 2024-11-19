using Involved.HTF.Common.Dto;
using System.Net.Http.Json;

namespace Involved.HTF.Common.Services
{
    public class CosmicStoneService
    {

        private readonly HackTheFutureClient _client;

        public CosmicStoneService(HackTheFutureClient client)
        {
            _client = client;
        }

        //public async Task<CommandDto> GetSampleChallengeOne()
        //{
        //    var response = await _client.GetAsync($"/api/a/easy/puzzle");

        //    if (!response.IsSuccessStatusCode)
        //        throw new Exception("You weren't able to log in, did you provide the correct credentials?");

        //    return await response.Content.ReadFromJsonAsync<CommandDto>();
        //}
    }
}
