using Everglades.Library.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Everglades.Library.Services
{
    public class InventoryServiceProxy
    {
        private static InventoryServiceProxy? instance;
        private static object instanceLock = new object();

        private List<Product> products;

        //prevents willy nilly access to IDS
        public ReadOnlyCollection<Product> Products
        { get { return products.AsReadOnly(); } }

        private int NextID
        {
            get
            {
                if (!products.Any())
                {
                    return 1;
                }
                return products.Select(p => p.Id).Max() + 1;
            }
        }

        public Product AddOrUpdate(Product p)
        {
            if (p == null)
            {
                return null;
            }
            bool isAdd = false;
            if(p.Id == 0)
            {
                isAdd = true;
                p.Id = NextID;
            }
            if(isAdd)
            {
                products.Add(p);    
            }
            return p;
        }


        private InventoryServiceProxy()
        {
            products = new List<Product>
            {
                new Product{Name = "Banana", Id = 1, Description="Its a Banana", Price=20, Quantity = 1},
                new Product{Name = "Orange", Id = 2, Description="Its a Orange", Price=5, Quantity = 1000}
            };
        }

        public static InventoryServiceProxy Current
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new InventoryServiceProxy();
                    }
                }
                return instance;
            }
        }
    }
}
