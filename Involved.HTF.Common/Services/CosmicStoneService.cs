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

        public async Task<AlienMailDto> GetSampleChallenge()
        {
            var response = await _client.GetAsync($"/api/b/easy/puzzle");

            if (!response.IsSuccessStatusCode)
                throw new Exception("You weren't able to log in, did you provide the correct credentials?");

            return await response.Content.ReadFromJsonAsync<AlienMailDto>();

        }

        public async Task<string> PostAnswer(string result)
        {
            var postRequest = await _client.PostAsJsonAsync($"/api/b/easy/puzzle", result);

            if (!postRequest.IsSuccessStatusCode)
                throw new Exception("You weren't able to log in, did you provide the correct credentials?");

            return await postRequest.Content.ReadAsStringAsync();
        }

        public string ConvertSymbolsToLetters(string input)
        {
            var symbolDictionary = GetAlienDictionary();

            var reverseDictionary = new Dictionary<string, string>();
            foreach (var pair in symbolDictionary)
            {
                reverseDictionary[pair.Value] = pair.Key;
            }

            var result = "";

            foreach (var symbol in input)
            {
                var symbolStr = symbol.ToString();
                if (reverseDictionary.ContainsKey(symbolStr))
                {
                    result += reverseDictionary[symbolStr];
                }
                else
                {
                    result += symbolStr;
                }
            }

            return result;
        }

        public static Dictionary<string, string> GetAlienDictionary()
        {
            return new Dictionary<string, string>
        {
            { "A", "∆" },
            { "B", "⍟" },
            { "C", "◊" },
            { "D", "Ψ" },
            { "E", "Σ" },
            { "F", "ϕ" },
            { "G", "Ω" },
            { "H", "λ" },
            { "I", "ζ" },
            { "J", "Ϭ" },
            { "K", "ↄ" },
            { "L", "◯" },
            { "M", "⧖" },
            { "N", "⊗" },
            { "O", "⊕" },
            { "P", "∇" },
            { "Q", "⟁" },
            { "R", "⎍" },
            { "S", "φ" },
            { "T", "✦" },
            { "U", "⨅" },
            { "V", "ᚦ" },
            { "W", "ϡ" },
            { "X", "⍾" },
            { "Y", "⍝" },
            { "Z", "≈" }
        };
        }

    }
}
