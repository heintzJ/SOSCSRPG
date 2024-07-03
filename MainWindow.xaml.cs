using System.Diagnostics;
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
using Engine.ViewModels;

namespace WPFUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// Create a new GameSession object that will create a player object
    /// The _gameSession is used to provide data for the .xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GameSession _gameSession;
        public MainWindow()
        {
            InitializeComponent();

            _gameSession = new GameSession();

            DataContext = _gameSession;
        }

        private void OutOfBounds()
        {
            string messageBoxText = "There is no room in that direction";
            string caption = "Word Processor";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result;

            result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
        }

        private void GainXP(object sender, RoutedEventArgs e)
        {
            _gameSession.CurrentPlayer.ExperiencePoints += 10;
            Debug.WriteLine(_gameSession.CurrentPlayer.ExperiencePoints);
        }

        private void ChangeLocation(object sender, RoutedEventArgs e)
        {
            string tag = (string)((Button)sender).Tag;
            Tuple<int, int> coords = GetCoords(tag);
            int newX = _gameSession.CurrentLocation.XCoordinate + coords.Item1;
            int newY = _gameSession.CurrentLocation.YCoordinate + coords.Item2;
            Debug.WriteLine($"{newX} {newY}");
            if (_gameSession.CurrentWorld.LocationAt(newX, newY) != null)
            {
                _gameSession.CurrentLocation = _gameSession.CurrentWorld.LocationAt(newX, newY);
                Debug.WriteLine(_gameSession.CurrentLocation.Name);
                Debug.WriteLine($"{_gameSession.CurrentLocation.XCoordinate}, {_gameSession.CurrentLocation.YCoordinate}");
            }
            else
            {
                OutOfBounds();
            }
        }

        private static Tuple<int, int> GetCoords(string tag)
        {
            string[] parts = tag.Trim('(',')').Split(',');
            int first = int.Parse(parts[0]);
            int second = int.Parse(parts[1]);
            return Tuple.Create(first, second );
        }
    }
}