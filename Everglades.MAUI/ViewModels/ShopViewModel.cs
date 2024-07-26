using Everglades.Library.DTO;
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
    public class ShopViewModel : INotifyPropertyChanged
    {
         
        public ShopViewModel() { 
            InventoryQuery = string.Empty;

        }

        private string inventoryQuery;

        public string InventoryQuery
        {
            get
            {
                return inventoryQuery;
            }
            set
            {
                inventoryQuery = value;
                NotifyPropertyChanged();
            }
        }

        public List<ProductViewModel> Products
        {
            get
            {
                return InventoryServiceProxy.Current?.Products.Where(p => p.Quantity > 0).Where(p => p?.Name?.ToUpper().Contains(InventoryQuery.ToUpper()) ?? false
                ).Select(p => new ProductViewModel(p)).ToList()
                    ?? new List<ProductViewModel>();
            }
        }

        public List<ProductViewModel> ProductsInCart
        {
            get
            {
                return ShoppingCartServiceProxy.Current?.Cart?.Contents?.Where(p => p?.Name?.ToUpper().Contains(InventoryQuery.ToUpper()) ?? false
                ).Select(p => new ProductViewModel(p)).ToList()
                    ?? new List<ProductViewModel>();
            }
        }

        // private Product productToBuy;

        private ProductViewModel? productToBuy;

        public ProductViewModel? ProductToBuy
        {
            get => productToBuy;

            set
            {
                productToBuy = value;
                if(productToBuy != null && productToBuy.Model == null)
                {
                    productToBuy.Model = new ProductDTO();
                }
                else if(productToBuy != null && productToBuy.Model != null) {
                
                    productToBuy.Model = new ProductDTO(productToBuy.Model);
                    
                    
                }
               // NotifyPropertyChanged();
                
                
            }
        }

        //public ProductViewModel? ProductToBuy {get; set;}   



        public ShoppingCart Cart {


            get
            {
                return ShoppingCartServiceProxy.Current.Cart;
            }
        }


        public void Refresh()
        {
            InventoryQuery = string.Empty;
            NotifyPropertyChanged(nameof(Products));
            NotifyPropertyChanged(nameof(ProductToBuy));
        }

        public void Search()
        {
            NotifyPropertyChanged(nameof(Products));
        }

        public void AddToCart()
        {
            //remove from Inventory
            if (ProductToBuy?.Model == null)
            {
                return;
            }
            ProductToBuy.Model = new ProductDTO(ProductToBuy.Model);
            //remove from inventory
            ProductToBuy.Model.Quantity = 1;
            ShoppingCartServiceProxy.Current.AddToCart(ProductToBuy.Model);
            
            NotifyPropertyChanged(nameof(ProductsInCart));
            NotifyPropertyChanged(nameof(Products));
            NotifyPropertyChanged(nameof(CartTotal)); 
            //add to cart

        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public decimal CartTotal
        {
            get
            {
                return ProductsInCart?.Sum(p => p.Model?.Price * p.Model?.Quantity) ?? 0;
            }
        }

    }
}
