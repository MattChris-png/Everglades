using Everglades.Library.Models;
using Everglades.Library.Services;
using Everglades.MAUI.ViewModels;
using Everglades.MAUI.Views;

namespace Everglades.MAUI.Views;



public partial class Shop : ContentPage
{
	public Shop()
	{
		InitializeComponent();
        BindingContext = new ShopViewModel();
	}

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }
    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as ShopViewModel)?.Refresh();
    }

    private void InventorySearchClicked(object sender, EventArgs e)
    {
        (BindingContext as ShopViewModel)?.Search();

    }

    private void AddToCartClicked(object sender, EventArgs e)
    {
        (BindingContext as ShopViewModel)?.AddToCart();

    }
}