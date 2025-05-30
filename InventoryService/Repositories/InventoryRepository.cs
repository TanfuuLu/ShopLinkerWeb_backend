﻿using InventoryService.Context;
using InventoryService.Interfaces;
using InventoryService.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Repositories;

public class InventoryRepository : IInventoryRepository {
	private readonly InventoryDbContext db;

	public InventoryRepository(InventoryDbContext context) {
		this.db = context;
	}

	public async Task<bool> AddItemAsync(InventoryItem item) {
		await db.InventoryItems.AddAsync(item);
		var result = await db.SaveChangesAsync();
		if (result > 0) {
			return true;
		}
		else {
			return false;
		}
	}

	public async Task<bool> AddListItemToShopAsync(List<int> lstItemID, int shopID) {
		var checkLstItem = await db.InventoryItems.Where(x => lstItemID.Contains(x.ItemID)).ToListAsync();
		if (checkLstItem.Count == 0) {
			return false;
		}
		foreach (var item in checkLstItem) {
			item.ShopID.Add(shopID);
		}
		var result = await db.SaveChangesAsync();
		if (result > 0) {
			return true;
		}
		else {
			return false;

		}
	}
	public async Task<bool> DeleteItemAsync(int id) {
		var checkItemExisted = await db.InventoryItems.FirstOrDefaultAsync(i => i.ItemID == id);
		if (checkItemExisted == null) {
			return false;
		}
		db.InventoryItems.Remove(checkItemExisted);
		var result = await db.SaveChangesAsync();
		if (result > 0) {
			return true;
		}
		else {
			return false;
		}
	}

	public async Task<ICollection<InventoryItem>> GetAllItemsAsync() {
		var lstItem = await db.InventoryItems.ToListAsync();
		if (lstItem.Count == 0) {
			return null;
		}
		return lstItem;
	}

	public async Task<ICollection<InventoryItem>> GetAllItemsByCategoryIDAsync(int categoryID) {
		var lstItem = await db.InventoryItems.Where(x => x.CategoryID == categoryID).ToListAsync();
		if (lstItem == null) {
			return null;
		}
		else {
			return lstItem;
		}
	}

	public async Task<ICollection<InventoryItem>> GetAllItemsByShopIDAsync(int shopID) {
		var lstItem = await db.InventoryItems.Where(x => x.ShopID.Contains(shopID)).ToListAsync();
		if (lstItem == null) {
			return null;
		}
		return lstItem;
	}

	public async Task<InventoryItem?> GetItemByIdAsync(int id) {
		var checkItem = await db.InventoryItems.FirstOrDefaultAsync(i => i.ItemID == id);
		if (checkItem == null) {
			return null;
		}
		return checkItem;
	}

	public async Task<ICollection<InventoryItem>> GetItemByListID(List<int> lstItemID) {
		var lstitem = await db.InventoryItems.Where(x => lstItemID.Contains(x.ItemID)).ToListAsync();
		if (lstitem == null) {
			return null;
		}
		return lstitem;

	}

	public async Task<InventoryItem?> GetItemByNameAsync(string name) {
		var checkItem = await db.InventoryItems.FirstOrDefaultAsync(i => i.ItemName.ToLower() == name.ToLower());
		if (checkItem == null) {
			return null;
		}
		return checkItem;
	}

	public async Task<bool> UpdateItemAsync(InventoryItem itemInput, int id) {
		var item = await db.InventoryItems.FirstOrDefaultAsync(i => i.ItemID == id);
		if (item == null) {
			return false;
		}
		item.Quantity = itemInput.Quantity;
		item.ShopID = itemInput.ShopID;
		item.Price = itemInput.Price;
		item.ItemName = itemInput.ItemName;
		item.TypeItem = itemInput.TypeItem;
		item.CategoryID = itemInput.CategoryID;
		
		var result = await db.SaveChangesAsync();
		if (result > 0) {
			return true;
		}
		else {
			return false;
		}
	}
}
