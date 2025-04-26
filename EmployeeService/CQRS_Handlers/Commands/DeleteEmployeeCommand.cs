using EmployeeService.Interfaces;
using Mediator;

namespace EmployeeService.CQRS_Handlers.Commands;

public record DeleteEmployeeCommand(int employeeID): IRequest<bool> {
}
public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, bool> {
	private readonly IEmployeeRepository employeeRepository;

	public DeleteEmployeeCommandHandler(IEmployeeRepository employeeRepository) {
		this.employeeRepository = employeeRepository;
	}

	public async ValueTask<bool> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken) {
		var result = await employeeRepository.DeleteEmployee(request.employeeID);
		return result;
	}
}
