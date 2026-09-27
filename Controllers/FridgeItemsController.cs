using Microsoft.AspNetCore.Mvc;
using MKitchen.Models;
using MKitchen.Services;

namespace MKitchen.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FridgeItemsController : ControllerBase
    {
        private readonly IFridgeService _service;

        public FridgeItemsController(IFridgeService service)
        { 
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<FridgeItem>> GetFridgeItems() => Ok(_service.GetFridgeItems());

        [HttpGet("{id}")]
        public ActionResult<FridgeItem> GetFridgeItemById(int id)
        {
            var fridgeItem = _service.GetFridgeItemById(id);
            return fridgeItem is null ? NotFound() : Ok(fridgeItem);
        }

        [HttpPost]
        public ActionResult<FridgeItem> CreateFridgeItem(FridgeItem fridgeItem) => 
            CreatedAtAction(nameof(GetFridgeItemById), new { id = fridgeItem.Id }, _service.AddFridgeItem(fridgeItem));

        [HttpPut("{id}")]
        public IActionResult UpdateFridgeItem(int id, FridgeItem fridgeItem) =>
            _service.UpdateFridgeItem(id, fridgeItem) ? NoContent() : NotFound();

        [HttpDelete("{id}")]
        public IActionResult DeleteFridgeItem(int id) =>
            _service.DeleteFridgeItem(id) ? NoContent() : NotFound();

        [HttpGet("/expiring")]
        public ActionResult<FridgeItem> GetSoonestExpiringFridgeItem()
        {
            var fridgeItem = _service.GetSoonestExpiringActiveFridgeItem();
            return fridgeItem is null ? NotFound() : Ok(fridgeItem);
        }

        [HttpGet("/expiring/{days}")]
        public ActionResult<List<FridgeItem>> GetSoonestExpiringActiveFridgeItem(int amount) => Ok(_service.GetSoonestExpiringActiveFridgeItems(amount));

        [HttpGet("/expired")]
        public ActionResult<List<FridgeItem>> GetExpiredFridgeItems() => Ok(_service.GetExpiredFridgeItems());
    }
}
