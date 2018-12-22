using System;
using System.Collections.Generic;
using System.Linq;
using GroceryHelper.Models;

namespace GroceryHelper.Helpers
{
    public static class ListHelpers
    {    
		public static void UpdateItem(Item item, IList<Item> listOfItems)
		{
			var foundItem = listOfItems.FirstOrDefault(x => x.Id == item.Id);
			foundItem.Name = item.Name;
			foundItem.Aisle = item.Aisle;
			foundItem.Id = item.Id;
			foundItem.Notes = item.Notes;
			foundItem.Type = item.Type;
		}

		public static void RemoveItem(Item item, IList<Item> listOfItems)
        {
            var foundItem = listOfItems.FirstOrDefault(x => x.Id == item.Id);
			listOfItems.Remove(foundItem);
        }

		public static bool ItemIsDuplicate(Item item, IList<Item> listOfItems)
		{
			return listOfItems.First(x => x.Name == item.Name) != null;
		}
    }
}
