namespace ShopService.Models.DTO;

public class UpdateShopRequest {
	public int Id { get; set; }
	public string ManagerName { get; set; }
	public string ManagerPhoneNumber { get; set; }
	public string ManagerEmail { get; set; }
	public string ShopImage { get; set; }
	public ICollection<int> Employee { get; set; }
}
