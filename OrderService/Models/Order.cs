using System.ComponentModel.DataAnnotations;

namespace OrderService.Models;

public class Order {
	[Key]
	public int OrderID { get; set; }
	public string? OrderCode { get; set; }
	public string? OrderTime { get; set; }
	public int UserOrderID { get; set; }
	public int TotalAmount { get; set; }
	public int ShopID { get; set; }
	public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
