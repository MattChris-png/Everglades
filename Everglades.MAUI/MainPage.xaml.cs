using Everglades.MAUI.ViewModels;

namespace Everglades.MAUI
{
    public partial class MainPage : ContentPage
    {
     
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();        
        }

        private void Clicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//Shopping Cart");
        }

        private void ManageInventoryClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//Inventory");
        }
        private void ShopClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//Shop");
        }
    }

}
