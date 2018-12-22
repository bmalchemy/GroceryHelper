using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GroceryHelper.Models;

namespace GroceryHelper.Services
{
    public interface IGroceryItemDataService
    {
		Task<IList<Item>> GetItemsAsync();
		Task<Item> GetItemAsync(string id);
		Task<Item> AddItemAsync(Item item);
		Task<Item> UpdateItemAsync(Item item);
		Task RemoveItemAsync(Item item);
    }
}
