using Everglades.API.EC;
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
        public async Task<IEnumerable<Product>> Get()
        {
            return await new InventoryEC().Get();
        }
    }
}
