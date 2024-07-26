using Everglades.Library.Models;
using Everglades.Library.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Everglades.MAUI.ViewModels
{
    public class InventoryViewModel : INotifyPropertyChanged
    {
        public List<ProductViewModel> Products
        {
            get
            {
                return InventoryServiceProxy.Current?.Products.Select(p => new ProductViewModel(p)).ToList()
                    ?? new List<ProductViewModel>();
            }
        }
        public ProductViewModel SelectedProduct { get; set; }
        public InventoryViewModel() { }

        public async void Refresh()
        {
            await InventoryServiceProxy.Current.Get();
            NotifyPropertyChanged(nameof(Products));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName ="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void UpdateProduct()
        {
            if(SelectedProduct == null || SelectedProduct.Model == null )
            {
                return;
            }
            InventoryServiceProxy.Current.AddOrUpdate(SelectedProduct.Model);
            InventoryServiceProxy.Current.Get();
        }

        public void Edit()
        {
            Shell.Current.GoToAsync($"//Product?productId={SelectedProduct?.Model?.Id ?? 0}");
        }

        public async void Delete()
        {
            await InventoryServiceProxy.Current.Delete(SelectedProduct?.Model?.Id ?? 0);
            Refresh();
        }
        public InventoryViewModel(Product c)
        {
            //product = c;
        }
    
    }

    
}
