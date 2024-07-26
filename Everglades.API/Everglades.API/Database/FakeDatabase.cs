using Everglades.Library.Models;

namespace Everglades.API.Database
{
    public static class FakeDatabase
    {
        public static List<Product> Products { get; set; } = new List<Product>
            {
                new Product{Name = "Banana", Id = 1, Description="Its a Banana", Price=20.25M, Quantity = 1},
                new Product{Name = "Orange", Id = 2, Description="Its a Orange", Price=5, Quantity = 1000},
                new Product{Name = "Key Lime", Id = 3, Description="From the Keys", Price=200.10M, Quantity=120}
            };



        public static int NextProductId
        {
            get
            {
                if (!Products.Any())
                {
                    return 1;
                }
                return Products.Select(p => p.Id).Max() + 1;
            }
        }

    }

}
