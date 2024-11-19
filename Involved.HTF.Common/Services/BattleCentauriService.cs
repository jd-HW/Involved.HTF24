using Involved.HTF.Common.Dto;
using Involved.HTF.Common.Dto.Battle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Involved.HTF.Common.Services
{
    public class BattleCentauriService
    {
        private readonly HackTheFutureClient _client;

        public BattleCentauriService(HackTheFutureClient client)
        {
            _client = client;
        }

        public async Task<BattleListDto> GetSampleChallenge()
        {
            var response = await _client.GetAsync($"api/a/medium/puzzle");

            if (!response.IsSuccessStatusCode)
                throw new Exception("You weren't able to log in, did you provide the correct credentials?");

            return await response.Content.ReadFromJsonAsync<BattleListDto>();
        }

        public async Task<string> PostSampleChallenge(WinningTeamDto winners)
        {

            var postRequest = await _client.PostAsJsonAsync($"/api/a/medium/puzzle", winners);
            var res = await postRequest.Content.ReadAsStringAsync();
            if (!postRequest.IsSuccessStatusCode)
                throw new Exception("You weren't able to log in, did you provide the correct credentials?");

            return res;
        }

        public WinningTeamDto ReturnWinningTeam(BattleListDto battleList)
        {
            // Extract the teams
            var teamAList = battleList.TeamA;
            var teamBList = battleList.TeamB;

            // Loop until one of the teams has no aliens left
            while (teamAList.Any() && teamBList.Any())
            {
                // Get the first alien from both teams
                var alienA = teamAList[0];
                var alienB = teamBList[0];

                // Determine who attacks first based on speed
                BattleTeamADto attackingAlien = null;
                BattleTeamADto defendingAlien = null;

                if (alienA.Speed >= alienB.Speed)
                {
                    attackingAlien = alienA;
                    defendingAlien = alienB;
                }
                else 
                {
                    attackingAlien = alienB;
                    defendingAlien = alienA;
                }

                // Simulate the attack
                // The attacking alien reduces the health of the defending alien
                defendingAlien.Health -= attackingAlien.Strength;

                // If the defending alien's health reaches zero or below, it is defeated
                if (defendingAlien.Health <= 0)
                {
                    // Remove the defeated alien from the list
                    if (defendingAlien == alienA)
                        teamAList.RemoveAt(0);
                    else
                        teamBList.RemoveAt(0);
                }
                else
                {
                    // If the defending alien survived, the attacking alien's health is reduced
                    attackingAlien.Health -= defendingAlien.Strength;

                    // If the attacking alien's health reaches zero or below, it is defeated
                    if (attackingAlien.Health <= 0)
                    {
                        // Remove the defeated attacking alien from the list
                        if (attackingAlien == alienA)
                            teamAList.RemoveAt(0);
                        else
                            teamBList.RemoveAt(0);
                    }
                }
            }

            // Determine the winning team
            WinningTeamDto winningTeam = new WinningTeamDto();

            if (teamAList.Any())
            {
                // Team A wins
                winningTeam.WinningTeam = "TeamA";
                winningTeam.RemainingHealth = teamAList.Sum(a => a.Health);
            }
            else
            {
                // Team B wins
                winningTeam.WinningTeam = "TeamB";
                winningTeam.RemainingHealth = teamBList.Sum(b => b.Health);
            }

            return winningTeam;
        }
    }
}
