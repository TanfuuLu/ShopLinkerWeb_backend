using EmployeeService.Context;
using EmployeeService.Interfaces;
using EmployeeService.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeService.Repositories;

public class EmployeeRepository : IEmployeeRepository {
	private readonly EmployeeDbContext dbContext;

	public EmployeeRepository(EmployeeDbContext dbContext) {
		this.dbContext = dbContext;
	}
	public async Task<bool> CreateEmployee(Employee employee) {
		if(employee == null) {
			return false;
		}
		await dbContext.Employees.AddAsync(employee);
		var result = await dbContext.SaveChangesAsync();
		if(result > 0) {
			return true;
		} else {
			return false;
		}
	}

	public async Task<bool> DeleteEmployee(int id) {
		if(id <= 0) {
			return false;
		}
		var item = await dbContext.Employees.FirstOrDefaultAsync(x => x.EmployeeId == id);
		if(item == null) {
			return false;
		}
		dbContext.Employees.Remove(item);
		var result = await dbContext.SaveChangesAsync();
		if(result > 0) {
			return true;
		} else {
			return false;
		}
	}

	public async Task<Employee> GetById(int id) {
		var empItem = await dbContext.Employees.FirstOrDefaultAsync(s => s.EmployeeId == id);
		if(empItem == null) {
			return null;
		}
		return empItem;
	}

	public async Task<ICollection<Employee>> GetEmployeesByShopId(int shopId) {
		var empList = await dbContext.Employees.Where(x => x.ShopId == shopId).ToListAsync();
		if(empList.Count > 0) {
			return empList;
		} else {
			return null;
		}
	}

	public async Task<ICollection<Employee>> ListEmployee() {
		var empList = await dbContext.Employees.ToListAsync();
		if(empList.Count > 0) {
			return empList;
		} else {
			return null;
		}
	}
}
