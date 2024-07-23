using Everglades.API.Database;
using Everglades.Library.DTO;
using Everglades.Library.Models;

namespace Everglades.API.EC
{
    public class InventoryEC
    {
        public InventoryEC()
        {
        }

        public async Task<IEnumerable<Product>> Get()
        {
            return FakeDatabase.Products.Take(100);
        }
    }
}
