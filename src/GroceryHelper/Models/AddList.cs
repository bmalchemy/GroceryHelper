using MvvmHelpers;

namespace GroceryHelper.Models
{
	public class AddList : ObservableObject
    {
		public string List { get; set; }

		public string[] Items { get; set; }

        public bool Done { get; set; }
    }
}