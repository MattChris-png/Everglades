using Everglades.Library.Models;
using Everglades.Library.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Everglades.MAUI.ViewModels
{
   
    public class ProductViewModel
    {
        public Product? Model { get; set; }
        public override string ToString()
        {
            return Model?.Name ?? "NA";
        }

        public ProductViewModel()
        {
            Model = new Product();
        }

        public ProductViewModel(int productId =0)
        {
            if(productId == 0)
            {
                Model = new Product();
            }
            else
            {
                Model = InventoryServiceProxy
                   .Current
                   .Products.FirstOrDefault(p => p.Id == productId)
                   ?? new Product();
            }
        }

        public ProductViewModel(Product c)
        {
            Model = c; 
        }

        public string PriceAsString
        {
            set
            {
                if (Model == null)
                {
                    return;
                }
                if(decimal.TryParse(value, out var price))
                {
                    Model.Price = price;
                }
                else
                {

                }
            }
        }

        public void Add()
        {
            if (Model != null)
            {
                InventoryServiceProxy.Current.AddOrUpdate(Model);
            }
        }
        
    }
}
