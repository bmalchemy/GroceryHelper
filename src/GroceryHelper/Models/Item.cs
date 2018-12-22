using System.ComponentModel;

namespace GroceryHelper.Models
{
	public class Item
    {        
		public string Id { get; set; }

		public string Name { get; set; }

		public string Aisle { get; set; }

		public string Type { get; set; }
        
		public double Latitude { get; set; }

		public double Longitude { get; set; }

		public string Notes { get; set; }
    }
}