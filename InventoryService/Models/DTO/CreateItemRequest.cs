using System.ComponentModel.DataAnnotations;

namespace InventoryService.Models.DTO;

public class CreateItemRequest {
	public string ItemName { get; set; }
	public int Quantity { get; set; }
	public int TypeItem { get; set; }
	public List<int>? ShopID { get; set; }
	public int CategoryID { get; set; }
}
