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

        public ProductViewModel(Product c)
        {
            Model = c; 
        }

        
    }
}
