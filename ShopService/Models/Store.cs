using System.ComponentModel.DataAnnotations;

namespace ShopService.Models;

public class Store {
	[Key]
	public int Id { get; set; }
	public string StoreName { get; set; }
	public string PhoneNumber { get; set; }
	public string Address { get; set; }
	public string ManagerName { get; set; }
	public string ManagerPhoneNumber { get; set; }
	public string ManagerEmail { get; set; }
	public string StoreImage { get; set; }
	public ICollection<int> Employee { get; set; }
}
