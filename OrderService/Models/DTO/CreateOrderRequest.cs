namespace OrderService.Models.DTO;

public class CreateOrderRequest {
	public string? OrderCode { get; set; }
	public string? OrderTime { get; set; }
	public int UserOrderID { get; set; }
	public int TotalAmount { get; set; }
	public int ShopID { get; set; }
}
