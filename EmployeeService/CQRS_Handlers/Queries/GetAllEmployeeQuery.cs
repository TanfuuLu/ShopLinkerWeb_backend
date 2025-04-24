using EmployeeService.Context;
using EmployeeService.Interfaces;
using EmployeeService.Models;
using Mediator;

namespace EmployeeService.CQRS_Handlers.Queries;

public record GetAllEmployeeQuery() : IRequest<ICollection<Employee>> {
}
public class GetAllEmployeeQueryHandler : IRequestHandler<GetAllEmployeeQuery, ICollection<Employee>> {
	private readonly IEmployeeRepository employeeRepository;

	public GetAllEmployeeQueryHandler(IEmployeeRepository employeeRepository) {
		this.employeeRepository = employeeRepository;
	}

	public async ValueTask<ICollection<Employee>> Handle(GetAllEmployeeQuery request, CancellationToken cancellationToken) {
		var listItem = await employeeRepository.ListEmployee();
		return listItem;
	}
}
