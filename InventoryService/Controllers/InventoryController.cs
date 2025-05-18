using InventoryService.CQRS.Command;
using InventoryService.CQRS.Queries;
using InventoryService.Models.DTO;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryService.Controllers;
[Route("api/[controller]")]
[ApiController]
public class InventoryController : ControllerBase {
	private readonly IMediator mediator;

	public InventoryController(IMediator mediator) {
		this.mediator = mediator;
	}
	[HttpGet("get-list-inventory-item")]
	public async Task<IActionResult> GetListInventoryItem( ) {
		var query = new GetAllInventoryItemQuery();
		var result = await mediator.Send(query);
		if (result == null) {
			return NotFound();
		}
		return Ok(result);
	}
	[HttpGet("get-inventory-item-of-shop/{id:int}")]
	public async Task<IActionResult> GetInventoryItemOfShop(int id) {
		var query = new GetAllInventoryItemByShopIdQuery(id);
		var result = await mediator.Send(query);
		if (result == null) {
			return NotFound();
		}
		return Ok(result);
	}
	[HttpGet("get-inventory-item/{id:int}")]
	public async Task<IActionResult> GetInventoryItem(int id) {
		var query = new GetItemByIdQuery(id);
		var result = await mediator.Send(query);
		if (result == null) {
			return NotFound();
		}
		return Ok(result);
	}
	[HttpGet("get-inventory-item-by-name/{name}")]
	public async Task<IActionResult> GetInventoryItemByName(string name) {
		var query = new GetItemByNameQuery(name);
		var result = await mediator.Send(query);
		if (result == null) {
			return NotFound();
		}
		return Ok(result);
	}
	[HttpGet("get-inventory-item-by-category/{categoryId:int}")]
	public async Task<IActionResult> GetInventoryItemByCategory(int categoryId) {
		var query = new GetItemByCategoryIdQuery(categoryId);
		var result = await mediator.Send(query);
		if (result == null) {
			return NotFound();
		}
		return Ok(result);
	}
	[HttpGet("get-item-by-list-item-id")]
	public async Task<IActionResult> GetItemByListId([FromBody] List<int> lstItemID) {
		var query = new GetItemByListIDQuery(lstItemID);
		var result = await mediator.Send(query);
		if (result == null) {
			return NotFound();
		}
		return Ok(result);
	}
	[HttpPost("create-inventory-item")]
	public async Task<IActionResult> CreateInventoryItem([FromBody] CreateItemRequest item) {
		if (item == null) {
			return BadRequest();
		}
		var command = new CreateItemCommand(item);
		var result = await mediator.Send(command);
		if (result == false) {
			return BadRequest();
		}
		return Ok(result);
	}
	[HttpPut("update-inventory-item/{id:int}")]
	public async Task<IActionResult> UpdateInventoryItem([FromBody] UpdateItemRequest itemRequest,[FromRoute]int id) {
		if (itemRequest == null) {
			return BadRequest();
		}
		var command = new UpdateItemCommand(itemRequest, id);
		var result = await mediator.Send(command);
		if (result == false) {
			return BadRequest();
		}
		return Ok(result);
	}
	[HttpDelete("delete-inventory-item/{id:int}")]
	public async Task<IActionResult> DeleteInventoryItem(int id) {
		var command = new DeleteItemCommand(id);
		var result = await mediator.Send(command);
		if (result == false) {
			return BadRequest();
		}
		return Ok(result);
	}
}
