using Engine.Models;
using Engine.ViewModels;
using System.Windows;

namespace WPFUI
{
    /// <summary>
    /// Interaction logic for TradeScreen.xaml
    /// </summary>
    public partial class TradeScreen : Window
    {
        // want to treat all data context as a GameSession object instead of casting
        public GameSession Session => DataContext as GameSession;
        public TradeScreen()
        {
            InitializeComponent();
        }

        private void OnClick_Sell(object sender, RoutedEventArgs e)
        {
            // get the DataContext of the row that was clicked on to find the item
            GameItem item = ((FrameworkElement)sender).DataContext as GameItem;
            if (item != null)
            {
                Session.CurrentPlayer.Gold += item.Price;
                Session.CurrentTrader.AddItemToInventory(item);
                Session.CurrentPlayer.RemoveItemFromInventory(item);
            }
        }

        private void OnClick_Buy(object sender, RoutedEventArgs e)
        {
            GameItem item = ((FrameworkElement)sender).DataContext as GameItem;
            if (item != null)
            {
                if (Session.CurrentPlayer.Gold >= item.Price)
                {
                    Session.CurrentPlayer.Gold -= item.Price;
                    Session.CurrentTrader.RemoveItemFromInventory(item);
                    Session.CurrentPlayer.AddItemToInventory(item);
                }
                else
                {
                    MessageBox.Show("You do not have enough gold");
                }
            }
        }
        private void OnClick_Close(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
