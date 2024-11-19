using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Involved.HTF.Common.Dto.Battle
{
    public class BattleListDto
    {
        public List<BattleTeamADto> TeamA { get; set; }
        public List<BattleTeamBDto> TeamB { get; set; }
    }
}
