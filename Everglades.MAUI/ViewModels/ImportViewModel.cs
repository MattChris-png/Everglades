using Everglades.Library.Services;
using Everglades.Library.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Everglades.MAUI.ViewModels
{
    public class ImportViewModel
    {
        public string FilePath { get; set; }

        public async void ImportFile(Stream? stream = null)
        {
            StreamReader? sr = null;
            try
            {
                if (stream == null) { sr = new StreamReader(FilePath); }
                else
                {
                    sr = new StreamReader(stream);
                }

            }
            catch (Exception ex)
            {
                return;
            }
            string line = string.Empty;
            var products = new List<ProductDTO>();
            while ((line = sr.ReadLine()) != null)
            {
                var tokens = line.Split(['|']);

                products.Add(new ProductDTO
                {
                    Name = tokens[0],
                    Description = tokens[1],
                    Price = decimal.Parse(tokens[2]),
                    Quantity = int.Parse(tokens[3])
                });
            }

            foreach (var product in products)
            {
                await InventoryServiceProxy.Current.AddOrUpdate(product);
            }
        }
    }
}