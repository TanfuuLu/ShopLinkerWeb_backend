using EmployeeService.Interfaces;
using EmployeeService.Models;
using EmployeeService.Models.DTO;
using MapsterMapper;
using Mediator;

namespace EmployeeService.CQRS_Handlers.Commands;

public record CreateEmployeeCommand(CreateEmployeeDTO model) : IRequest<bool> {
}
public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, bool> {
	private readonly IMapper mapper;
	private readonly IEmployeeRepository employeeRepository;

	public CreateEmployeeCommandHandler(IMapper mapper, IEmployeeRepository employeeRepository) {
		this.mapper = mapper;
		this.employeeRepository = employeeRepository;
	}

	public async ValueTask<bool> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken) {
		if(request == null) {
			throw new Exception("Input is null");
		}
		var itemDTO = mapper.Map<Employee>(request.model);
		var result = await employeeRepository.CreateEmployee(itemDTO);
		if(result == true) {
			return true;
		} else {
			return false;
		}
	}
}
