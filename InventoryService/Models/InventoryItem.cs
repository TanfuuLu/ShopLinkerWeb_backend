using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryService.Models;

public class InventoryItem {
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int ItemID {  get; set; }
	[Required]
	public string ItemName {  get; set; }
	[Required]
	[Range(0,100)]
	public int Quantity { get; set; }
	[Required]
	public TypeItemEnum TypeItem {  get; set; }
	public List<int>? ShopID { get; set; } = new List<int>();
	public int CategoryID { get; set; }
	public Category Category { get; set; }

}
