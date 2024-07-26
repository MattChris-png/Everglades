using Everglades.API.EC;
using Everglades.Library.DTO;
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

        [HttpGet()]
        public async Task<IEnumerable<ProductDTO>> Get()
        {
            return await new InventoryEC().Get();
        }


        [HttpDelete("/{id}")]
        public async Task<ProductDTO?> Delete(int id)
        {
            return await new InventoryEC().Delete(id);
        }

        [HttpPost()]
        public async Task<ProductDTO> AddOrUpdate([FromBody] ProductDTO p)
        {
            return await new InventoryEC().AddOrUpdate(p);
        }
    }
}
