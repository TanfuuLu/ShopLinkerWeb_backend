using EmployeeService.Interfaces;
using EmployeeService.Models;
using Mediator;

namespace EmployeeService.CQRS_Handlers.Queries;

public record GetListEmployeeByShopIDQuery(int shopID) : IRequest<ICollection<Employee>>{
}
public class GetListEmployeeByShopIDQueryHandler : IRequestHandler<GetListEmployeeByShopIDQuery, ICollection<Employee>> {
	private readonly IEmployeeRepository employeeRepository;

	public GetListEmployeeByShopIDQueryHandler(IEmployeeRepository employeeRepository) {
		this.employeeRepository = employeeRepository;
	}

	public async ValueTask<ICollection<Employee>> Handle(GetListEmployeeByShopIDQuery request, CancellationToken cancellationToken) {
		var listItem = await employeeRepository.GetEmployeesByShopId(request.shopID);
		if (listItem == null) {
			return null;
		}
		else {
			return listItem;
		}

	}
}
