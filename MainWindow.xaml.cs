using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Engine.EventArgs;
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

            _gameSession.OnMessageRaised += OnGameMessageRaised;

            DataContext = _gameSession;
        }

        private void MoveLocation(object sender, RoutedEventArgs e)
        {
            if ((string)((Button)sender).Content == "North") {
                _gameSession.MoveNorth();
            }
            else if ((string)((Button)sender).Content == "East")
            {
                _gameSession.MoveEast();
            }
            else if ((string)((Button)sender).Content == "South")
            {
                _gameSession.MoveSouth();
            }
            else
            {
                _gameSession.MoveWest();
            }
        }

        private void OnClick_AttackMonster(object sender, RoutedEventArgs e)
        {
            _gameSession.AttackCurrentMonster();
        }

        private void OnGameMessageRaised(object sender, GameMessageEventArgs e)
        {
            // show the message
            GameMessages.Document.Blocks.Add(new Paragraph(new Run(e.Message)));
            GameMessages.ScrollToEnd();
        }
    }
}