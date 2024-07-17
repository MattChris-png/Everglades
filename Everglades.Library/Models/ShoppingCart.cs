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
        public List<Product>? Contents { get; set; }

    }
}
