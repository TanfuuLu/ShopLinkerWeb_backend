using MapsterMapper;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopService.Handlers.Commands;
using ShopService.Handlers.Queries;
using ShopService.Models.DTO;

namespace ShopService.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ShopController : ControllerBase {
	private readonly IMediator _mediator;
	private readonly IMapper _mapper;
	public ShopController(IMediator mediator, IMapper mapper) {
		this._mediator = mediator;
		this._mapper = mapper;
	}
	[HttpPost("create-shop")]
	public async Task<IActionResult> CreateShop([FromBody] CreateShopRequest model) {
		var command = new CreateShopCommand(model);
		var result = await _mediator.Send(command);
		if(result != null) {
			return Ok(result);
		} else {
			return BadRequest("Failed to create shop");	
		}
	}
	[HttpGet("get-all-shop")]
	public async Task<IActionResult> GetAllShop() {
		var query = new GetAllShopQuery();
		var result = await _mediator.Send(query);
		if(result != null) {
			return Ok(result);
		} else {
			return BadRequest("Failed to get all shop");
		}
	}
	[HttpPut("update-shop")]
	public async Task<IActionResult> UpdateShop([FromBody] UpdateShopRequest model) {
		var command = new UpdateShopCommand(model);
		var result = await _mediator.Send(command);
		if(result != null) {
			return Ok(result);
		} else {
			return BadRequest("Failed to update shop");
		}

	}
	[HttpGet("get-shop-by-id/{id}")]
	public async Task<IActionResult> GetById([FromRoute] int id) {
		var query = new GetShopByIdQuery(id);
		var result = await _mediator.Send(query);
		if(result != null) {
			return Ok(result);
		} else {
			return BadRequest("Failed to get shop by id");
		}
	}
	[HttpPut("shop/{id:int}/add-employee/{idEmp:int}")]
	public async Task<IActionResult> AddEmployeeToShop([FromRoute] int id, [FromRoute] int idEmp) {
		var command = new AddEmployeeToShopCommand(id, idEmp);
		var result = await _mediator.Send(command);
		if(result != null) {
			return Ok(result);
		} else {
			return BadRequest("Failed to add employee to shop");
		}
	}
	[HttpDelete("shop/{id:int}/remove-employee/{idEmp:int}")]
	public async Task<IActionResult> RemoveEmployeeToShop([FromRoute] int id, [FromRoute] int idEmp) {
		var command = new RemoveEmployeeFromShopCommand(id, idEmp);
		var result = await _mediator.Send(command);
		if(result != null) {
			return Ok(result);
		} else {
			return BadRequest("Failed to remove employee from shop");
		}
	}
}
