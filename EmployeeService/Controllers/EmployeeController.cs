using EmployeeService.CQRS_Handlers.Commands;
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
	[HttpPost("create")]
	public async Task<IActionResult> CreateEmployee([FromBody]CreateEmployeeDTO model) {
		var command = new CreateEmployeeCommand(model);
		var result = await mediator.Send(command);
		if(result == null) {
			return BadRequest("Failed to create employee");
		} else {
			return Ok(result);
		}
	}

}
