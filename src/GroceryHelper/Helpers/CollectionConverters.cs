using System.Collections.Generic;
using GroceryHelper.Models;
using MvvmHelpers;

namespace GroceryHelper.Helpers
{
    public static class CollectionConverters
    {
		public static ObservableRangeCollection<ToBuyItem> ConvertToObservable(IList<Item> items)
        {
            var newObservable = new ObservableRangeCollection<ToBuyItem>();

            foreach (var i in items)
            {
                var toBuy = new ToBuyItem
                {
                    Name = i.Name,
					Aisle = i.Aisle,
                    Id = i.Id,
                    Notes = i.Notes
                };
				newObservable.Add(toBuy);
            }
			return newObservable;
        }

		public static ToBuyItem ConvertToToBuyItem(Item item) 
		{
			var newToBuy = new ToBuyItem
            {
				Name = item.Name,
				Aisle = item.Aisle,
				Id = item.Id,
				Notes = item.Notes
            };
			return newToBuy;
		}
    }
}
