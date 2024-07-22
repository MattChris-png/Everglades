using Everglades.Library.Models;
using Microsoft.AspNetCore.Mvc;
namespace Everglades.API.Controllers
{

    [ApiController]
    [Route("[controller]")] 
    public class InventoryController : ControllerBase
    {

        private readonly ILogger<InventoryController> _logger;


        public InventoryController(ILogger<InventoryController> logger)
        {
            _logger =logger;
        }

        [HttpGet(Name = "GetInventory")]
        public IEnumerable<Product> Get()
        {
            return new List<Product>
            {
                new Product{Name = "Banana", Id = 1, Description="Its a Banana", Price=20.25M, Quantity = 1},
                new Product{Name = "Orange", Id = 2, Description="Its a Orange", Price=5, Quantity = 1000},
                new Product{Name = "Key Lime", Id = 3, Description="From the Keys", Price=200.10M, Quantity=120}
            };
        }
    }
}
