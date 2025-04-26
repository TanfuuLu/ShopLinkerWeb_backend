using System.ComponentModel.DataAnnotations;

namespace InventoryService.Models.DTO;

public class CreateItemRequest {
	public string ItemName { get; set; }
	public int Quantity { get; set; }
	public TypeItemEnum TypeItem { get; set; }
	public int? ShopID { get; set; }
	public int CategoryID { get; set; }
}
