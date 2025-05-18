namespace OrderService.Models.DTO;

public class CreateOrderItemRequest {
	public string ItemName { get; set; }
	public int Quantity { get; set; }
	public int Price { get; set; }
	public int TotalPrice { get; set; }
	public int TotalAmount { get; set; }
	public int OrderId { get; set; }
}
