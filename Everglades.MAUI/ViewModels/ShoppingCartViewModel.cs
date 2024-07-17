using Everglades.Library.Models;
using Everglades.Library.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Everglades.MAUI.ViewModels
{
    public class ShoppingCartViewModel
    {
        public List<Product> Products
        { 
            get
            {
                return InventoryServiceProxy.Current?.Products.ToList() ?? new List<Product>();
            }
        }
        public Product SelectedProduct { get; set; } 
        public ShoppingCartViewModel() { }
        public void UpdateProduct()
        {
            InventoryServiceProxy.Current.AddOrUpdate(SelectedProduct);

        }
    }

    
}
