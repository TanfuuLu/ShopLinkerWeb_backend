namespace ShopService.Models.DTO;

public class CreateUpdateShopResponse {
	public string ShopName { get; set; }
	public string PhoneNumber { get; set; }
	public string Address { get; set; }
	public string ManagerName { get; set; }
	public string ManagerPhoneNumber { get; set; }
	public string ManagerEmail { get; set; }
	public string ShopImage { get; set; }
	public ICollection<int> Employee { get; set; }
}
