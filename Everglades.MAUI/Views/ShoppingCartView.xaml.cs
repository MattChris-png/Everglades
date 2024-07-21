using Everglades.Library.Models;
using Everglades.Library.Services;
using Everglades.MAUI.ViewModels;

namespace Everglades.MAUI.Views;

public partial class ShoppingCartView : ContentPage
{
	public ShoppingCartView()
	{
		InitializeComponent();
		BindingContext = new InventoryViewModel(); 
	}

	public void EditClicked(Object sender, EventArgs e)
	{
		(BindingContext as InventoryViewModel).UpdateProduct();
	}

	private void BackClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

}