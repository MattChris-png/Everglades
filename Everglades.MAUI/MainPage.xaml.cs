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

    }

}
