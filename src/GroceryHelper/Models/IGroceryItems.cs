using System.Collections.Generic;

namespace GroceryHelper.Models
{
    public interface IGroceryItems
	{
		IList<Item> GItems { get; set; }
    }
}
