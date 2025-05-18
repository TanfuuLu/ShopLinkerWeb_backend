namespace InventoryService.Models.DTO;

public class UpdateItemRequest {
	public string ItemName { get; set; }
	public int Quantity { get; set; }
	public int Price { get; set; }
	public TypeItemEnum TypeItem { get; set; }
	public List<int>? ShopID { get; set; }
	public int CategoryID { get; set; }
}
