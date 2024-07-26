using Everglades.Library.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Everglades.Library.Models
{
    public class ShoppingCart
    {
        int ID { get; set; }
        public List<ProductDTO>? Contents { get; set; }

        public ShoppingCart() { 
            Contents = new List<ProductDTO>();
        }
    }
}
