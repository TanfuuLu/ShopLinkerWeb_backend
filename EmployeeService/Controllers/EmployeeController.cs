using EmployeeService.CQRS_Handlers.Commands;
using EmployeeService.CQRS_Handlers.Queries;
using EmployeeService.Models.DTO;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Controllers;
[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase {
	private readonly IMediator mediator;

	public EmployeeController(IMediator mediator) {
		this.mediator = mediator;
	}
	// Create Emplotee API
	[HttpPost("create-employee")]
	public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeDTO model) {
		var command = new CreateEmployeeCommand(model);
		var result = await mediator.Send(command);
		if (result == null) {
			return BadRequest("Failed to create employee");
		} else {
			return Ok(result);
		}
	}
	// Delete Employee API

	[HttpDelete("delete/{id:int}")]
	public async Task<IActionResult> DeleteEmployee([FromRoute] int id) {
		var command = new DeleteEmployeeCommand(id);
		var result = await mediator.Send(command);
		if (result == null) {
			return NotFound("Not found employee");
		}
		else {
			return Ok(result);
		}
	}

	/////////////////////////////////////////////////////////////////////////////////
	///Get Data API
	[HttpGet("shop/{id:int}/get-all-employee")]
	public async Task<IActionResult> GetAllEmployeeByShopID([FromRoute] int id) {
		var query = new GetListEmployeeByShopIDQuery(id);
		var result = await mediator.Send(query);
		if (result == null) {
			return NotFound("Not have employee");
		}
		else {
			return Ok(result);
		}
	}
	[HttpGet("list-employee")]
	public async Task<IActionResult> GetListEmployee() {
		var query = new GetAllEmployeeQuery();
		var result = await mediator.Send(query);
		if (result == null) {
			return NotFound("Not have employee");
		}
		else {
			return Ok(result);
		}
	}
}
