using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeService.Models;

public class Employee {
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int EmployeeId { get; set; }
	public string Name { get; set; }
	public string PhoneNumber { get; set; }
	[DatabaseGenerated(DatabaseGeneratedOption.Computed)]

	public DateTime? StarterDate { get; set; }
	public int ShopId { get; set; }
}
