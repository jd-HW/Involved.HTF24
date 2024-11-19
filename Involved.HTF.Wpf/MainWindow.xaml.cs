using Involved.HTF.Common;
using Involved.HTF.Common.Services;
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

        const string TeamName = "Tune-Squad";
        const string Password = "340cf9e7-fcf2-46d7-adc8-468487c35eaf";

        string puzzleString = string.Empty;

        public MainWindow()
        {
            InitializeComponent();
            client = new HackTheFutureClient();
            _aqualonService = new AqualonService(client);
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

        private void BtnGetCentauri_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnPostCentauri_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}