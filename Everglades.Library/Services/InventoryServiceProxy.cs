﻿using Everglades.Library.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Everglades.Library.Utilities;

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
                new Product{Name = "Banana", Id = 1, Description="Its a Banana", Price=20.25M, Quantity = 1},
                new Product{Name = "Orange", Id = 2, Description="Its a Orange", Price=5, Quantity = 1000},
                new Product{Name = "Key Lime", Id = 3, Description="From the Keys", Price=200.10M, Quantity=120}
            };

            var response = new WebRequestHandler().Get("/Inventory").Result;
            products = JsonConvert.DeserializeObject<List<Product>>(response);
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

        public Product Delete(int id)
        {
            var itemToDelete = Products.FirstOrDefault(p => p.Id == id);
            if(itemToDelete == null)
            {
                return null;
            }

            products.Remove(itemToDelete);
            return itemToDelete;
        }
    }
}
