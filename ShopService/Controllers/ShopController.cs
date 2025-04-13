using MapsterMapper;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopService.Handlers.Commands;
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

}
