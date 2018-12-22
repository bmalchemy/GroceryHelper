using System;
using System.Collections.Generic;
using Prism.Ioc;

namespace GroceryHelper.Models
{
	public class GroceryItems : IGroceryItems
	{
		public IList<Item> GItems { get; set; }      
	}

}
