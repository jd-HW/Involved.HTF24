using Involved.HTF.Common;
using Involved.HTF.Common.Dto.Battle;
using Involved.HTF.Common.Services;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Involved.HTF.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HackTheFutureClient client;
        AqualonService _aqualonService;
        BattleCentauriService _battleCentauriService;

        const string TeamName = "Tune-Squad";
        const string Password = "340cf9e7-fcf2-46d7-adc8-468487c35eaf";

        private List<BattleTeamADto> teamA;
        private List<BattleTeamBDto> teamB;

        string puzzleString = string.Empty;

        public MainWindow()
        {
            InitializeComponent();
            client = new HackTheFutureClient();
            _aqualonService = new AqualonService(client);
            _battleCentauriService = new BattleCentauriService(client);
        }

        private async void BtnGet_Click(object sender, RoutedEventArgs e)
        {
            await client.Login(TeamName, Password);
            puzzleString = (await _aqualonService.GetSampleChallengeOne()).Commands;
            txtResult.Text = puzzleString;
        }

        private async void BtnPost_Click(object sender, RoutedEventArgs e)
        {
            var result = _aqualonService.CalculatePosition(puzzleString);

            var response = await _aqualonService.PostSampleChallengeOne(result);
            txtPostContent.Text = response;
        }

        private void BtnCosmicGet_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnCosmicPost_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void BtnGetCentauri_Click(object sender, RoutedEventArgs e)
        {
            await client.Login(TeamName, Password);
            var p = (await _battleCentauriService.GetSampleChallenge());
            teamB = p.TeamB;
            teamA = p.TeamA;

            List<string> stringA = new();
            List<string> stringB = new();

            for (int i = 0; i < teamA.Count; i++)
            {
                stringA.Add($"AlienA{i + 1}: Strength: {teamA[i].Strength}, Speed: {teamA[i].Speed}, Health: {teamA[i].Health}");
            }

            // Process Team B
            for (int i = 0; i < teamB.Count; i++)
            {
                stringB.Add($"AlienB{i + 1}: Strength: {teamB[i].Strength}, Speed: {teamB[i].Speed}, Health: {teamB[i].Health}");
            }

            txtCentaurGet.Text = String.Join(",", stringA.Concat(stringB));
        }

        private async void BtnPostCentauri_Click(object sender, RoutedEventArgs e)
        {
            var battleListDto = new BattleListDto { TeamA = teamA, TeamB = teamB };
            var result = _battleCentauriService.ReturnWinningTeam(battleListDto);

            var response = await _battleCentauriService.PostSampleChallenge(result);
            txtCentauriPost.Text = response;
        }
    }
}