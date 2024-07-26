using Everglades.Library.Models;
using Everglades.Library.Services;
using Everglades.MAUI.ViewModels;


namespace Everglades.MAUI.Views;

public partial class Inventory : ContentPage
{
	public Inventory()
	{
        InitializeComponent();
        BindingContext = new InventoryViewModel();
    }
        

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

    private void AddClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Product");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as InventoryViewModel)?.Refresh();
    }

    private void EditClicked(object sender, EventArgs e)
    {
        (BindingContext as InventoryViewModel)?.Edit();
    }

    private void DeleteClicked(object sender, EventArgs e)
    {
        (BindingContext as InventoryViewModel)?.Delete();
    }

    private void SearchClicked(object sender, EventArgs e)
    {
        (BindingContext as InventoryViewModel)?.Search();
    }
}