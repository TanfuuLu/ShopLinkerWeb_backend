using System.ComponentModel.DataAnnotations;

namespace OrderService.Models;

public class OrderItem {
	[Key]
	public int OrderItemID { get; set; }
	public string ItemName { get; set; }
	public int Quantity { get; set; }
	public int Price { get; set; }
	public int TotalPrice { get; set; }
	public int OrderId { get; set; }
	public Order Order { get; set; }
}
