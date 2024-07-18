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
            GroupedInventoryItem item = ((FrameworkElement)sender).DataContext as GroupedInventoryItem;
            if (item != null && item.Quantity > 0)
            {
                Session.CurrentPlayer.ReceiveGold(item.Item.Price);
                Session.CurrentTrader.AddItemToInventory(item.Item);
                Session.CurrentPlayer.RemoveItemFromInventory(item.Item);
            }
        }

        private void OnClick_Buy(object sender, RoutedEventArgs e)
        {
            GroupedInventoryItem item = ((FrameworkElement)sender).DataContext as GroupedInventoryItem;
            if (item != null)
            {
                if (Session.CurrentPlayer.Gold >= item.Item.Price)
                {
                    Session.CurrentPlayer.SpendGold(item.Item.Price);
                    Session.CurrentTrader.RemoveItemFromInventory(item.Item);
                    Session.CurrentPlayer.AddItemToInventory(item.Item);
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
