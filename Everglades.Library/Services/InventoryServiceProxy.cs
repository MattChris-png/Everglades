using Everglades.Library.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Everglades.Library.Utilities;
using Everglades.Library.DTO;

namespace Everglades.Library.Services
{
    public class InventoryServiceProxy
    {
        private static InventoryServiceProxy? instance;
        private static object instanceLock = new object();

        private List<ProductDTO> products;

        //prevents willy nilly access to IDS
        public ReadOnlyCollection<ProductDTO> Products
        { get { return products.AsReadOnly(); } }

        //private int NextID
        //{
        //    get
        //    {
        //        if (!products.Any())
        //        {
        //            return 1;
        //        }
        //        return products.Select(p => p.Id).Max() + 1;
        //    }
        //}

        public async Task<ProductDTO> AddOrUpdate(ProductDTO p)
        {


            var result = await new WebRequestHandler().Post("/Inventory", p);
            //Get();
            return JsonConvert.DeserializeObject<ProductDTO>(result);
        }

        public async Task<IEnumerable<ProductDTO>> Get()
        {
            var result = await new WebRequestHandler().Get("/Inventory");
            var deserializedResult = JsonConvert.DeserializeObject<List<ProductDTO>>(result);
            products = deserializedResult?.ToList() ?? new List<ProductDTO>();
            return products;
        }

        private InventoryServiceProxy()
        {
            var response = new WebRequestHandler().Get("/Inventory").Result;
            products = JsonConvert.DeserializeObject<List<ProductDTO>>(response);
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

        public async Task<ProductDTO?> Delete(int id)
        {
            //var itemToDelete = Products.FirstOrDefault(p => p.Id == id);
            //if(itemToDelete == null)
            //{
            //    return null;
            //}

            //products.Remove(itemToDelete);
            //return itemToDelete;
            var response = await new WebRequestHandler().Delete($"/{id}");
            var itemToDelete = JsonConvert.DeserializeObject<ProductDTO>(response);
            return itemToDelete;
        }
    }
}
