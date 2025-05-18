using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderService.CQRS.Command;
using OrderService.CQRS.Query;
using OrderService.Interfaces;
using OrderService.Models.DTO;

namespace OrderService.Controllers;
[Route("api/[controller]")]
[ApiController]
public class OrderItemController : ControllerBase {
	private readonly IMediator mediator;
	public OrderItemController(IMediator mediator) {
		this.mediator = mediator;
	}
	[HttpPost("CreateOrderItem")]
	public async Task<IActionResult> CreateOrderItem([FromBody] CreateOrderItemRequest model) {
		var command = new CreateOrderItemCommand(model);
		var result = await mediator.Send(command);
		if (result) {
			return Ok("Created Order Item ");
		}
		else {
			return BadRequest();
		}
	}
	[HttpPost("CreateListOrderItem")]
	public async Task<IActionResult> CreateListOrderItem([FromBody] List<CreateOrderItemRequest> listItem) {
		var command = new CreateListOrderItemCommand(listItem);
		var result = await mediator.Send(command);
		if (result) {
			return Ok("Created List Order Item ");
		}
		else {
			return BadRequest();
		}
	}
	[HttpGet("GetOrderItemByOrder/{orderId}")]
	public async Task<IActionResult> GetOrderItemByOrder(int orderId) {
		var query = new GetOrderItemByOrderQuery(orderId);
		var result = await mediator.Send(query);
		if (result != null) {
			return Ok(result);
		}
		else {
			return NotFound();
		}
	}
}
