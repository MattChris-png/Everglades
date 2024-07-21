using Everglades.Library.Models;
using Everglades.Library.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Everglades.MAUI.ViewModels
{
    public class InventoryViewModel
    {
        public List<ProductViewModel> Products
        {
            get
            {
                return InventoryServiceProxy.Current?.Products.Select(c => new ProductViewModel(c)).ToList()
                    ?? new List<ProductViewModel>();
            }
        }
        public ProductViewModel SelectedProduct { get; set; }
        public InventoryViewModel() { }
        public void UpdateProduct()
        {
            if(SelectedProduct.Model == null)
            {
                return;
            }
            InventoryServiceProxy.Current.AddOrUpdate(SelectedProduct.Model);

        }

        public InventoryViewModel(Product c)
        {
            //product = c;
        }
    
    }

    
}
