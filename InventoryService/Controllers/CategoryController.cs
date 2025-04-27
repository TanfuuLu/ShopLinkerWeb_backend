using InventoryService.CQRS.CategoryCQRS.Command;
using InventoryService.CQRS.CategoryCQRS.Query;
using InventoryService.Models.DTO;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryService.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase {
	private readonly IMediator mediator;

	public CategoryController(IMediator mediator) {
		this.mediator = mediator;
	}
	[HttpGet("get-all-category")]
	public async Task<IActionResult> GetAllCategory() {
		var query = new GetAllCategoryQuery();
		var result = await mediator.Send(query);
		if (result == null) {
			return NotFound();
		}
		return Ok(result);
	}
	[HttpGet("get-category-by-id/{id}")]
	public async Task<IActionResult> GetCategoryById(int id) {
		var query = new GetCategoryByIdQuery(id);
		var result = await mediator.Send(query);
		if (result == null) {
			return NotFound();
		}
		return Ok(result);
	}
	[HttpPost("create-category")]
	public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequest model) {
		var command = new CreateCategoryCommand(model);
		var result = await mediator.Send(command);
		if (result == false) {
			return BadRequest();
		}
		return Ok(result);
	}
	[HttpDelete("delete-category/{id}")]
	public async Task<IActionResult> DeleteCategory(int id) {
		var command = new DeleteCategoryCommand(id);
		var result = await mediator.Send(command);
		if (result == false) {
			return BadRequest();
		}
		return Ok(result);
	}
}
