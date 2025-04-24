using EmployeeService.Models;

namespace EmployeeService.Interfaces;

public interface IEmployeeRepository {
	Task<bool> CreateEmployee(Employee employee);
	Task<bool> DeleteEmployee(int id);
	Task<ICollection<Employee>> ListEmployee();
	Task<Employee> GetById(int id);
	Task<ICollection<Employee>> GetEmployeesByShopId(int shopId);

}
