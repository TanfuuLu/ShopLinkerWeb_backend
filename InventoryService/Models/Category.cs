using System.ComponentModel.DataAnnotations;

namespace InventoryService.Models;

public class Category {
	[Key]
	public int CategoryID { get; set; }
	public string? CategoryName { get; set; }
}
